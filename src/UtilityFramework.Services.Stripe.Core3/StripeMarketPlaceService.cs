using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Stripe;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Stripe.Core3.Interfaces;
using UtilityFramework.Services.Stripe.Core3.Models;
using File = Stripe.File;

namespace UtilityFramework.Services.Stripe.Core3
{
    public partial class StripeMarketPlaceService(IHostingEnvironment env) : StripeBaseService(env), IStripeMarketPlaceService
    {
        public async Task<StripeBaseResponse<Account>> GetByIdAsync(string accountId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();
                var requestOptions = new RequestOptions
                {
                    StripeAccount = accountId
                };

                return await service.GetAsync(accountId, null, requestOptions);
            });
        }

        public async Task<StripeBaseResponse<Account>> GetPlatformAsync()
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();
                var platform = await service.GetSelfAsync();
                return platform;
            });
        }

        public async Task<StripeBaseResponse<Account>> CreateFullOptionsAsync(AccountCreateOptions options)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();
                return await service.CreateAsync(options);
            });
        }

        public async Task<StripeBaseResponse<Account>> UpdateAsync(string accountId, StripeMarketPlaceRequest data)
        {
            return await HandleActionAsync(async () =>
            {
                if (string.IsNullOrWhiteSpace(accountId))
                    throw new CustomError("ID da subconta não informado.");

                var service = new AccountService();
                var account = await service.GetAsync(accountId);
                if (account == null)
                    throw new CustomError("Conta não encontrada.");

                if (data.AcceptTerms == null)
                    throw new CustomError("Informe se o usuário aceitou os termos de uso.");

                if (string.IsNullOrEmpty(data.Email))
                    throw new CustomError("Email não informado.");

                var scheduleValidations = new Dictionary<EStripePayoutSchedule, (Func<StripeMarketPlaceRequest, object> Property, string Error)>
                {
                    { EStripePayoutSchedule.Weekly, (d => d.WeeklyAnchor, "Informe o dia da semana que deve ser realizado saque dos valores dessa conta.") },
                    { EStripePayoutSchedule.Monthly, (d => d.MonthlyAnchor, "Informe o dia do mês que deve ser realizado saque dos valores dessa conta.") }
                };

                if (scheduleValidations.TryGetValue(data.PayoutSchedule, out var validation) && validation.Property(data) == null)
                    throw new CustomError(validation.Error);

                if (data.PayoutSchedule == EStripePayoutSchedule.Manual && data.Country.EqualsIgnoreCase("BR"))
                    throw new CustomError("Pagamento manual inválido para conta BR.");

                var optionsCreate = new AccountCreateOptions();
                var options = new AccountUpdateOptions
                {
                    Capabilities = data.Capabilities,
                    BusinessType = data.BusinessType.ToString().ToLower(),
                    Email = data.Email,
                    BusinessProfile = new AccountBusinessProfileOptions
                    {
                        Name = data.BusinessName,
                        SupportEmail = data.SupportEmail ?? data.Email ?? data.Individual?.Email,
                        ProductDescription = data.ProductDescription ?? "Plataforma de serviços digitais",
                        Mcc = data.Mcc ?? "5734",
                        SupportPhone = data.Individual?.Phone.MapPhone(),
                    },
                    Settings = new AccountSettingsOptions
                    {
                        Payouts = new AccountSettingsPayoutsOptions
                        {
                            Schedule = new AccountSettingsPayoutsScheduleOptions
                            {
                                Interval = data.PayoutSchedule.GetEnumMemberValue(),
                                MonthlyAnchor = data.MonthlyAnchor,
                                WeeklyAnchor = data.WeeklyAnchor?.GetEnumMemberValue(),
                                DelayDays = data.PayoutDelayDays
                            }
                        }
                    }
                };

                if (data.BankAccount != null)
                    options.ExternalAccount = CreateBankAccountOptions(data.BankAccount);

                var (skipUploadDocumentCompany, skipUploadDocumentIndividual) = account.Requirements.CurrentlyDue.GetSkipDocuments();

                switch (data.BusinessType)
                {
                    case EStripeBusinessType.Individual:
                        if (data.Individual == null)
                            throw new CustomError("Campo Individual é obrigatório.");
                        await ConfigureIndividualOptions(optionsCreate, data.Individual, skipUploadDocumentIndividual);

                        options.Individual = optionsCreate.Individual;
                        break;
                    case EStripeBusinessType.Company:
                        if (data.Company == null)
                            throw new CustomError("Campo Company é obrigatório.");
                        if (data.Company.Representative == null)
                            throw new CustomError("Campo Company.Representative é obrigatório.");

                        await ConfigureCompanyOptions(optionsCreate, data.Company, skipUploadDocumentCompany);

                        options.Company = optionsCreate.Company;
                        break;
                    default:
                        throw new CustomError("Tipo de empresa não configurado.");
                }

                account = await service.UpdateAsync(accountId, options);

                if (account.BusinessType == EStripeBusinessType.Company.GetEnumMemberValue())
                {
                    var personOptions = await ConfigurePersonOptions(data.Company.Representative, skipUploadDocumentIndividual);

                    var persons = await service.Persons.ListAsync(accountId);

                    var existingRepresentative = persons.Data.FirstOrDefault(x => x.Relationship?.Representative == true);

                    if (existingRepresentative != null)
                    {
                        var updateOptions = new AccountPersonUpdateOptions();

                        updateOptions.SetIfDifferent(personOptions);

                        await service.Persons.UpdateAsync(accountId, existingRepresentative.Id, updateOptions);
                    }
                    else
                    {
                        await service.Persons.CreateAsync(accountId, personOptions);
                    }
                }

                return await service.GetAsync(accountId);
            });
        }

        public async Task<StripeBaseResponse<Account>> UpdateExternalAccountAsync(string accountId, AccountBankAccountOptions dataBankOptions)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();

                return await service.UpdateAsync(accountId, new AccountUpdateOptions()
                {
                    ExternalAccount = dataBankOptions
                });
            });
        }

        public async Task<StripeBaseResponse<Account>> DeleteAsync(string accountId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();

                return await service.DeleteAsync(accountId);
            });
        }

        public async Task<StripeBaseResponse<Account>> RejectAsync(string accountId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();
                return await service.RejectAsync(accountId);
            });
        }

        public async Task<StripeBaseResponse<StripeList<Account>>> ListAsync()
        {
            return await HandleActionAsync(async () =>
            {
                var service = new AccountService();
                return await service.ListAsync();
            });
        }

        public async Task<StripeBaseResponse<File>> UploadFileAsync(string path, EStripeFilePurpose filePurpose = EStripeFilePurpose.IdentityDocument)
        {
            return await HandleActionAsync(async () =>
            {

                if (string.IsNullOrEmpty(path))
                    throw new CustomError("Caminho do arquivo não informado.");

                path = path.ResolveFilePath();

                if (!System.IO.File.Exists(path))
                    throw new CustomError($"Arquivo não encontrado em: {path}");

                var fileInfo = new FileInfo(path);
                if (fileInfo.Length > 10 * 1024 * 1024)
                    throw new CustomError("O arquivo deve ter menos de 10MB.");

                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".pdf" };
                if (!allowedExtensions.Contains(fileInfo.Extension.ToLower()))
                    throw new CustomError("Formato de arquivo inválido. Permitido: JPG, PNG ou PDF.");

                var fileService = new FileService();

                using var stream = new FileStream(path, FileMode.Open);

                var file = await fileService.CreateAsync(new FileCreateOptions
                {
                    Purpose = filePurpose.GetEnumMemberValue(),
                    File = stream
                });

                return file;
            });
        }

        public async Task<StripeBaseResponse<Account>> CreateAsync(StripeMarketPlaceRequest data, EStripeAccountType accountType = EStripeAccountType.Express, AccountCapabilitiesOptions capabilitiesOptions = null)
        {
            return await HandleActionAsync(async () =>
            {
                if (data.AcceptTerms == null)
                    throw new CustomError("Informe se o usuário aceitou os termos de uso.");

                if (string.IsNullOrEmpty(data.Email))
                    throw new CustomError("Email não informado.");

                var scheduleValidations = new Dictionary<EStripePayoutSchedule, (Func<StripeMarketPlaceRequest, object> Property, string Error)>
                {
                    { EStripePayoutSchedule.Weekly, (d => d.WeeklyAnchor, "Informe o dia da semana que deve ser realizado saque dos valores dessa conta.") },
                    { EStripePayoutSchedule.Monthly, (d => d.MonthlyAnchor, "Informe o dia do mês que deve ser realizado saque dos valores dessa conta.") }
                };

                if (scheduleValidations.TryGetValue(data.PayoutSchedule, out var validation) && validation.Property(data) == null)
                    throw new CustomError(validation.Error);

                if (data.PayoutSchedule == EStripePayoutSchedule.Manual && data.Country.EqualsIgnoreCase("BR"))
                    throw new CustomError("Pagamento manual inválido para conta BR.");

                capabilitiesOptions ??= new AccountCapabilitiesOptions
                {
                    Transfers = new AccountCapabilitiesTransfersOptions { Requested = true },
                    CardPayments = new AccountCapabilitiesCardPaymentsOptions { Requested = true },
                    BoletoPayments = new AccountCapabilitiesBoletoPaymentsOptions { Requested = true }
                };

                var options = new AccountCreateOptions
                {
                    Type = accountType.GetEnumMemberValue().ToLower(),
                    Country = data.Country.OrDefaultCountry(),
                    Controller = data.Controller,
                    Capabilities = capabilitiesOptions,
                    BusinessType = data.BusinessType.ToString().ToLower(),
                    Email = data.Email,
                    BusinessProfile = new AccountBusinessProfileOptions
                    {
                        Name = data.BusinessName,
                        SupportEmail = data.SupportEmail ?? data.Email ?? data.Individual?.Email,
                        ProductDescription = data.ProductDescription ?? "Plataforma de serviços digitais",
                        Mcc = data.Mcc ?? "5734",
                        SupportPhone = data.Individual?.Phone.MapPhone(),
                    },
                    Settings = new AccountSettingsOptions
                    {
                        Payouts = new AccountSettingsPayoutsOptions
                        {
                            Schedule = new AccountSettingsPayoutsScheduleOptions
                            {
                                Interval = data.PayoutSchedule.GetEnumMemberValue(),
                                MonthlyAnchor = data.MonthlyAnchor,
                                WeeklyAnchor = data.WeeklyAnchor?.GetEnumMemberValue(),
                                DelayDays = data.PayoutDelayDays
                            }
                        }
                    }
                };

                if (data.AcceptTerms == true)
                {
                    options.Type = null;
                    options.Controller ??= new AccountControllerOptions();
                    options.Controller.RequirementCollection = "application";
                    options.Controller.Fees ??= new AccountControllerFeesOptions { Payer = "application" };
                    options.Controller.StripeDashboard ??= new AccountControllerStripeDashboardOptions { Type = EStripeDashboardType.None.GetEnumMemberValue() };
                    options.Controller.Losses ??= new AccountControllerLossesOptions { Payments = "application" };
                    options.TosAcceptance = new AccountTosAcceptanceOptions
                    {
                        Date = DateTime.Now,
                        Ip = data.RemoteIp,
                        UserAgent = data.UserAgent,
                        ServiceAgreement = "full"
                    };
                }

                if (data.BankAccount != null)
                    options.ExternalAccount = CreateBankAccountOptions(data.BankAccount);

                switch (data.BusinessType)
                {
                    case EStripeBusinessType.Individual:
                        if (data.Individual == null)
                            throw new CustomError("Campo Individual é obrigatório.");
                        await ConfigureIndividualOptions(options, data.Individual);
                        break;
                    case EStripeBusinessType.Company:
                        if (data.Company == null)
                            throw new CustomError("Campo Company é obrigatório.");
                        if (data.Company.Representative == null)
                            throw new CustomError("Campo Company.Representative é obrigatório.");
                        await ConfigureCompanyOptions(options, data.Company);
                        break;
                    default:
                        throw new CustomError("Tipo de empresa não configurado.");
                }

                var service = new AccountService();
                var account = await service.CreateAsync(options);

                if (data.BusinessType == EStripeBusinessType.Company)
                {
                    try
                    {
                        var personOptions = await ConfigurePersonOptions(data.Company.Representative);
                        await service.Persons.CreateAsync(account.Id, personOptions);
                        account = await service.GetAsync(account.Id);
                    }
                    catch
                    {
                        await service.DeleteAsync(account.Id);
                        throw;
                    }
                }

                return account;
            });
        }

        public async Task<StripeBaseResponse<BankAccount>> GetBankAccountAsync(string accountId)
        {
            return await HandleActionAsync(async () =>
            {
                var accountService = new AccountService();

                var account = await accountService.GetAsync(accountId);

                if (account == null)
                    return null;

                var bankAccountId = account.ExternalAccounts.Data.FirstOrDefault()?.Id;

                if (string.IsNullOrEmpty(bankAccountId))
                    throw new CustomError("Nenhuma conta bancária foi vinculada a essa conta.");

                var bankAccountService = new AccountExternalAccountService();

                return await bankAccountService.GetAsync(account.Id, bankAccountId) as BankAccount;
            });
        }

        public AccountBankAccountOptions CreateBankAccountOptions(StripeExternalAccountMarketPlaceRequest bankAccount)
        {
            if (bankAccount == null)
                return null;
            return new AccountBankAccountOptions
            {
                Object = "bank_account",
                Country = bankAccount.Country.OrDefaultCountry(),
                Currency = bankAccount.Currency.OrDefaultCurrency(),
                AccountNumber = bankAccount.AccountNumber,
                RoutingNumber = $"{bankAccount.BankCode}-{bankAccount.AgencyNumber}",
                AccountHolderName = bankAccount.HolderName,
                AccountHolderType = bankAccount.HolderType.GetEnumMemberValue().ToLower(),
            };
        }

        public async Task ConfigureIndividualOptions(AccountCreateOptions options, StripeIndividualRequest individual, bool skipUploadDocument = false)
        {
            options.Individual = new AccountIndividualOptions
            {
                FirstName = individual.FirstName,
                LastName = individual.LastName,
                Email = individual.Email,
                Phone = individual.Phone.MapPhone(),
                IdNumber = individual.Cpf?.OnlyNumbers(),
                Address = individual.Address.MapAddress(),
                Gender = individual.Gender?.GetEnumMemberValue(),
                PoliticalExposure = "none",
                Relationship = new AccountIndividualRelationshipOptions
                {
                    Director = true,
                    Executive = true,
                    Owner = true,
                    Title = "owner",
                    PercentOwnership = 100
                }
            };

            if (!string.IsNullOrEmpty(individual.BirthDate))
            {
                var (day, month, year) = PartsOfValidDate(individual.BirthDate, "Individual.BirthDate");
                options.Individual.Dob = new DobOptions { Day = day, Month = month, Year = year };
            }

            if (skipUploadDocument)
                return;

            if (individual.RequiredDocument && string.IsNullOrEmpty(individual.DocumentFront))
                throw new CustomError("Campo Individual.DocumentFront é obrigatório.");

            if (!string.IsNullOrEmpty(individual.DocumentFront) || !string.IsNullOrEmpty(individual.DocumentBack))
            {
                options.Individual.Verification = new() { Document = new() };

                if (!string.IsNullOrEmpty(individual.DocumentFront))
                {
                    var resultDocumentFront = await UploadFileAsync(individual.DocumentFront, EStripeFilePurpose.IdentityDocument);
                    if (!resultDocumentFront.Success)
                        throw new CustomError(resultDocumentFront.ErrorMessage);
                    options.Individual.Verification.Document.Front = resultDocumentFront?.Data?.Id;
                }

                if (!string.IsNullOrEmpty(individual.DocumentBack))
                {
                    var resultDocumentBack = await UploadFileAsync(individual.DocumentBack, EStripeFilePurpose.IdentityDocument);
                    if (!resultDocumentBack.Success)
                        throw new CustomError(resultDocumentBack.ErrorMessage);
                    options.Individual.Verification.Document.Back = resultDocumentBack?.Data?.Id;
                }
            }
        }

        public async Task<AccountPersonCreateOptions> ConfigurePersonOptions(StripeIndividualRequest representative, bool skipUploadDocument = false)
        {
            if (representative.RequiredDocument && string.IsNullOrEmpty(representative.DocumentFront))
                throw new CustomError("Campo \"company.representative.documentFront\" é obrigatório.");

            var person = new AccountPersonCreateOptions
            {
                FirstName = representative.FirstName,
                LastName = representative.LastName,
                Email = representative.Email,
                Phone = representative.Phone.MapPhone(),
                IdNumber = representative.Cpf?.OnlyNumbers(),
                Address = representative.Address.MapAddress(),
                Gender = representative.Gender?.GetEnumMemberValue(),
                PoliticalExposure = "none",
                Relationship = new AccountPersonRelationshipOptions
                {
                    Executive = true,
                    Director = true,
                    Owner = true,
                    Representative = true,
                    PercentOwnership = 100,
                    Title = "owner",
                }
            };


            if (!string.IsNullOrEmpty(representative.BirthDate))
            {
                var (d, m, y) = PartsOfValidDate(representative.BirthDate, "Company.Representative.BirthDate");
                person.Dob = new AccountPersonDobOptions { Day = d, Month = m, Year = y };
            }

            if (skipUploadDocument)
                return person;

            if (!string.IsNullOrEmpty(representative.DocumentFront) || !string.IsNullOrEmpty(representative.DocumentBack))
            {
                person.Verification = new() { Document = new() };

                if (!string.IsNullOrEmpty(representative.DocumentFront))
                {
                    var resultDocumentFront = await UploadFileAsync(representative.DocumentFront, EStripeFilePurpose.IdentityDocument);
                    if (!resultDocumentFront.Success)
                        throw new CustomError(resultDocumentFront.ErrorMessage);
                    person.Verification.Document.Front = resultDocumentFront?.Data?.Id;
                }

                if (!string.IsNullOrEmpty(representative.DocumentBack))
                {
                    var resultDocumentBack = await UploadFileAsync(representative.DocumentBack, EStripeFilePurpose.IdentityDocument);
                    if (!resultDocumentBack.Success)
                        throw new CustomError(resultDocumentBack.ErrorMessage);
                    person.Verification.Document.Back = resultDocumentBack?.Data?.Id;
                }
            }
            return person;
        }

        public async Task ConfigureCompanyOptions(AccountCreateOptions options, StripeCompanyRequest company, bool skipUploadDocument = false)
        {
            if (company.RequiredDocument && string.IsNullOrEmpty(company.DocumentFront))
                throw new CustomError("Campo Company.DocumentFront é obrigatório.");

            options.Company = new AccountCompanyOptions
            {
                Name = company.LegalName,
                TaxId = company.Cnpj?.OnlyNumbers(),
                Phone = company.Phone.MapPhone(),
                Address = company.Address.MapAddress(),
                DirectorsProvided = true,
                OwnersProvided = true,
                ExecutivesProvided = true
            };

            if (skipUploadDocument)
                return;

            if (!string.IsNullOrEmpty(company.DocumentFront) || !string.IsNullOrEmpty(company.DocumentBack))
            {
                options.Company.Verification = new() { Document = new() };

                if (!string.IsNullOrEmpty(company.DocumentFront))
                {
                    var resultDocumentFront = await UploadFileAsync(company.DocumentFront, EStripeFilePurpose.IdentityDocument);
                    if (!resultDocumentFront.Success)
                        throw new CustomError(resultDocumentFront.ErrorMessage);
                    options.Company.Verification.Document.Front = resultDocumentFront?.Data?.Id;
                }

                if (!string.IsNullOrEmpty(company.DocumentBack))
                {
                    var resultDocumentBack = await UploadFileAsync(company.DocumentBack, EStripeFilePurpose.IdentityDocument);
                    if (!resultDocumentBack.Success)
                        throw new CustomError(resultDocumentBack.ErrorMessage);
                    options.Company.Verification.Document.Back = resultDocumentBack?.Data?.Id;
                }
            }
        }

        private (int day, int month, int year) PartsOfValidDate(string date, string fieldName)
        {
            try
            {
                var birthDate = date.TryParseAnyDate();
                if (((DateTime.Now - birthDate).Days / 365) < 18)
                    throw new CustomError("A idade mínima para cadastro é 18 anos.");
                return (birthDate.Day, birthDate.Month, birthDate.Year);
            }
            catch
            {
                throw new CustomError($"Data de nascimento no campo {fieldName} inválida.");
            }
        }
    }
}
