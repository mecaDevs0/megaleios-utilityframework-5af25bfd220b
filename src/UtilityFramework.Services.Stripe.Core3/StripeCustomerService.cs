using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Stripe;
using UtilityFramework.Services.Stripe.Core3.Interfaces;
using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3
{
    public class StripeCustomerService(IHostingEnvironment env) : StripeBaseService(env), IStripeCustomerService
    {
        public async Task<StripeBaseResponse<Customer>> FindCustomerByIdAsync(string customerId)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new CustomerService();
                var customer = await service.GetAsync(customerId);

                if (customer != null && customer.Deleted == false)
                    return customer;

                throw new CustomError("Cliente não encontrado");
            });
        }

        public async Task<StripeBaseResponse<Customer>> FindCustomerByDocumentAsync(string document)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new CustomerService();
                var options = new CustomerSearchOptions { Query = $"metadata['CPF_CNPJ']:'{document}'" };
                var customers = await service.SearchAsync(options);

                if (customers.Data.Count > 0)
                    return customers.Data.First();

                throw new CustomError("Cliente não encontrado");
            });
        }
        public async Task<StripeBaseResponse<Customer>> FindCustomerByEmailAsync(string email)
        {
            return await HandleActionAsync(async () =>
            {
                var service = new CustomerService();
                var options = new CustomerSearchOptions { Query = $"email:'{email}'" };
                var customers = await service.SearchAsync(options);

                if (customers.Data.Count > 0)
                    return customers.Data.First();

                throw new CustomError("Cliente não encontrado");
            });
        }

        public async Task<StripeBaseResponse<Customer>> CreateOrGetCustomerAsync(StripeCustomerRequest customerRequest)
        {
            return await HandleActionAsync(async () =>
            {
                if (!string.IsNullOrEmpty(customerRequest.CpfCnpj) || !string.IsNullOrEmpty(customerRequest.Email))
                {
                    var customerExists = await FindCustomerByDocumentAsync(customerRequest.CpfCnpj);

                    if (customerExists?.Data == null)
                        customerExists ??= await FindCustomerByEmailAsync(customerRequest.Email);

                    if (customerExists?.Data != null)
                    {
                        return customerExists.Data;
                    }
                }

                var service = new CustomerService();

                var customerOptions = new CustomerCreateOptions
                {
                    Name = customerRequest.FullName,
                    Email = customerRequest.Email,
                    Phone = customerRequest.Phone,
                    PaymentMethod = customerRequest.PaymentMethodId,
                    PreferredLocales = customerRequest.PreferredLocales
                };

                if (!string.IsNullOrEmpty(customerRequest.Address?.Street) && !string.IsNullOrEmpty(customerRequest.Address?.Number))
                {
                    customerOptions.Address = new AddressOptions
                    {
                        Line1 = $"{customerRequest.Address.Street}, {customerRequest.Address.Number}",
                        Line2 = customerRequest.Address.Complement,
                        City = customerRequest.Address.City,
                        State = customerRequest.Address.State,
                        PostalCode = customerRequest.Address.ZipCode,
                        Country = customerRequest.Address.Country
                    };
                }

                if (!string.IsNullOrEmpty(customerRequest.CpfCnpj))
                {
                    customerOptions.TaxIdData =
                    [
                        new()
                        {
                            Type = customerRequest.CpfCnpj.Length == 11 ? "br_cpf" : "br_cnpj",
                            Value = customerRequest.CpfCnpj
                        }
                    ];
                    customerOptions.Metadata = new Dictionary<string, string>
                    {
                        { "CPF_CNPJ", customerRequest.CpfCnpj }
                    };
                }

                return await service.CreateAsync(customerOptions);
            });
        }

        public async Task<StripeBaseResponse> DeleteCustomerAsync(string customerId)
        {
            return await HandleActionAsync(async () =>
            {
                if (string.IsNullOrEmpty(customerId))
                    throw new CustomError("CustomerId é obrigatório");

                var service = new CustomerService();
                return await service.DeleteAsync(customerId);
            });
        }


        public async Task<StripeBaseResponse<Customer>> UpdateCustomerAsync(StripeCustomerRequest customerRequest)
        {
            return await HandleActionAsync(async () =>
            {
                if (string.IsNullOrEmpty(customerRequest?.CustomerId))
                    throw new CustomError("CustomerId é obrigatório");

                var service = new CustomerService();

                return await service.UpdateAsync(customerRequest.CustomerId, new CustomerUpdateOptions()
                {
                    Name = customerRequest.FullName,
                    Email = customerRequest.Email,
                    Phone = customerRequest.Phone,
                    DefaultSource = customerRequest.PaymentMethodId,
                    Address = new AddressOptions
                    {
                        Line1 = $"{customerRequest.Address.Street}, {customerRequest.Address.Number}",
                        Line2 = customerRequest.Address.Complement,
                        City = customerRequest.Address.City,
                        State = customerRequest.Address.State,
                        PostalCode = customerRequest.Address.ZipCode,
                        Country = customerRequest.Address.Country
                    }
                });
            });
        }
    }
}