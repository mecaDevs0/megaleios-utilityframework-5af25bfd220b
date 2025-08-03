using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Authenticators;
using UtilityFramework.Application.Core3;
using UtilityFramework.Application.Core3.ViewModels;
using UtilityFramework.Services.Iugu.Core3.Entity;
using UtilityFramework.Services.Iugu.Core3.Models;
using UtilityFramework.Services.Iugu.Core3.Response;

namespace UtilityFramework.Services.Iugu.Core3
{
    public static class IuguUtility
    {

        /// <summary>
        /// REMOVE OPERAÇÃO CASO CONTA CAIXA
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="bank"></param>
        /// <returns></returns>
        public static string RemoveOperacao(this string conta, string bank)
        {
            bank = bank?.ToLower()?.TrimSpaces();

            if (string.IsNullOrEmpty(conta) == false && (bank == "caixaeconômica" || bank == "104") && (conta.StartsWith("013") || conta.StartsWith("001")) && conta.Length > 3)
                return conta?.Substring(3);

            return conta;
        }

        /// <summary>
        /// METODO PARA CALCULAR DIVISÃO DE VALORES (SPLIT)
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="value"></param>
        /// <param name="feeClient"></param>
        /// <param name="feeMega"></param>
        /// <param name="feeIugu"></param>
        /// <param name="feeAdvance"></param>
        /// <param name="hasAdvance"></param>
        /// <param name="apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public static FeeViewModel CalculeFees(string invoiceId, double value, double feeClient, double feeMega, double feeIugu = 2.51, double feeAdvance = 2.5, bool hasAdvance = false, string apiToken = null)
        {

            var response = new FeeViewModel();
            IuguInvoiceResponseMessage invoice = null;
            try
            {
                var iuguCharge = new IuguService();

                if (string.IsNullOrEmpty(invoiceId) == false)
                    invoice = iuguCharge.GetFaturaAsync(invoiceId, apiToken).Result;

                if (string.IsNullOrEmpty(invoiceId) == false && invoice == null)
                    throw new NullReferenceException("Fatura não encontrada");

                if (hasAdvance == false && invoice != null && invoice.FinancialReturnDates != null && invoice.FinancialReturnDates.Count > 0 && invoice.FinancialReturnDates[0].Advanced == true)
                    hasAdvance = true;

                if (invoice?.PaidCents != null)
                    value = invoice.PaidCents.GetValueOrDefault().ToReal();

                response.IuguValue = invoice?.TaxesPaidCents != null
                                     ? invoice.TaxesPaidCents.GetValueOrDefault().ToReal()
                                     : Utilities.GetValueOfPercent(value, feeIugu).NotAround();

                response.IuguAdvanceValue = invoice?.AdvanceFeeCents != null
                                            ? invoice.AdvanceFeeCents.GetValueOrDefault().ToReal()
                                            : hasAdvance == false ? 0 : Utilities.GetValueOfPercent(value, feeAdvance).NotAround();

                response.IuguFeesValue = (response.IuguValue + response.IuguAdvanceValue).NotAround();
                response.ClientValue = Utilities.GetValueOfPercent(value, feeClient).NotAround();
                response.MegaValue = Utilities.GetValueOfPercent(value, feeMega).NotAround();
                response.GrossValue = value;
                response.TotalFeesValue = (response.MegaValue + response.ClientValue + response.IuguValue + response.IuguAdvanceValue).NotAround();
                response.NetValue = (value - response.TotalFeesValue).NotAround();
            }
            catch (Exception ex)
            {
                response.Error = true;
                response.ErrorMessage = $"{ex.InnerException} {ex.Message}".Trim();
            }
            return response;

        }

        /// <summary>
        /// MAPEAMENTO DE ERRO
        /// </summary>
        /// <param name="response"></param>
        /// <param name="responseError"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IuguBaseMarketPlace MapErrorDataBank<T>(this IuguBaseMarketPlace response, T responseError) where T : IuguBaseErrors
        {
            try
            {
                response.Error = responseError.Error;
                response.HasError = true;
                response.MessageError = responseError.MessageError;
            }
            catch (Exception ex)
            {
                response.Error = new { error = $"{ex.InnerException} {ex.Message}".Trim() };
                response.HasError = true;
                response.MessageError = "Ocorreu um erro inesperado";
            }

            return response;
        }

        /// <summary>
        /// OBTER NOME DO BANCO PELO CODE
        /// </summary>
        /// <param name="bankCode"></param>
        /// <returns></returns>
        [Obsolete]
        public static string GetBankName(this string bankCode)
        {

            var banCodeNumbers = bankCode?.OnlyNumbers();
            if (string.IsNullOrEmpty(bankCode) == false && banCodeNumbers.Length > 0)
            {
                switch (banCodeNumbers)
                {
                    case "341":
                        return "Itaú";
                    case "237":
                        return "Bradesco";
                    case "104":
                        return "Caixa Econômica";
                    case "001":
                        return "Banco do Brasil";
                    case "033":
                        return "Santander";
                    case "041":
                        return "Banrisul";
                    case "748":
                        return "Sicredi";
                    case "756":
                        return "Sicoob";
                    case "077":
                        return "Inter";
                    case "070":
                        return "BRB";
                    case "085":
                        return "Via Credi";
                    case "655":
                        return "Neon";
                    case "260":
                        return "Nubank";
                    case "290":
                        return "Pagseguro";
                    case "212":
                        return "Banco Original";
                    case "422":
                        return "Safra";
                    case "364":
                        return "Gerencianet Pagamentos";
                    case "136":
                        return "Unicred";
                    case "021":
                        return "Banestes";
                    case "746":
                        return "Modal";

                    default:
                        return bankCode;
                }
            }

            return bankCode;

        }

        /// <summary>
        /// GERA RESPONSE DE ERRO
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static string MapErrors(this IRestResponse response)
        {
            switch (response.StatusCode)
            {
                case (HttpStatusCode)422:
                    var dataError = JsonConvert.DeserializeObject<IuguErrors422>(response.Content);

                    return dataError.Errors.FirstOrDefault().Value.FirstOrDefault();
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:

                    return JsonConvert.DeserializeObject<IuguError>(response.Content)?.Errors;

                case 0:
                    return "Não foi possivel estabelecer uma conexão com o servidor";
            }
            return null;
        }

        /// <summary>
        /// GERA RESPONSE DE ERRO
        /// </summary>
        /// <param name="response"></param>
        /// <param name="showContent"></param>
        /// <returns></returns>
        public static IuguBaseErrors IuguMapErrors<T>(this IRestResponse response, bool showContent) where T : IuguBaseErrors
        {
            var baseErrors = new IuguBaseErrors();

            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                case (HttpStatusCode)422:

                    IuguErrors422 dataError = null;

                    try
                    {
                        try
                        {
                            dataError = JsonConvert.DeserializeObject<IuguErrors422>(response.Content);
                        }
                        catch (Exception) {/*PREVENT STOP*/}

                        if (dataError == null)
                        {
                            var pattern = new Regex("(\"\\w+\":?(\".*?\"|\\[\".*?\"\\]))");

                            var matches = pattern.Matches(response.Content);

                            var allmatch = matches.Cast<Match>().Select(match =>
                            {
                                return match.Value.Contains("[") == false ? match.Value.Replace(":\"", ":[\"") + "]" : match.Value;
                            }).ToList();

                            var erro = $"{{\"errors\":{{{string.Join(", ", allmatch)}}}}}";

                            if (erro.ContainsIgnoreCase("{}"))
                                erro = erro.Replace("{}", "null");

                            dataError = JsonConvert.DeserializeObject<IuguErrors422>(erro);
                        }

                    }
                    catch (Exception)
                    {

                        if (string.IsNullOrEmpty(response.Content) == false && response.Content.Contains("errors\":") == false)
                        {
                            response.Content = $"{{\"errors\":{response.Content}}}";
                        }

                        dataError = JsonConvert.DeserializeObject<IuguErrors422>(response.Content);
                    }

                    var firstMessage = response.Content;
                    #region GET PROPERTY NAME

                    if (dataError.Errors != null && dataError.Errors.Count() > 0)
                    {

                        var properties = typeof(T).GetProperties().ToList();

                        var arrayField = dataError.Errors.FirstOrDefault().Key.Split('.');
                        var fieldName = string.Empty;

                        try
                        {
                            var countChildTypeClass = properties.Count(x => x.GetCustomAttribute<IsClass>() != null);

                            var propField = properties.Find(x => x.GetCustomAttribute<JsonPropertyAttribute>() != null && x.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == arrayField[0]);

                            var isPropertClass = propField != null && countChildTypeClass > 0;

                            if (isPropertClass)
                            {
                                var childOffClass = properties.Where(x =>
                                    x.GetCustomAttribute<IsClass>() != null).ToList();

                                for (var i = 0; i < countChildTypeClass; i++)
                                {
                                    var propChild = childOffClass[i].PropertyType.GetProperties().FirstOrDefault(x =>
                                        x.GetCustomAttribute<JsonPropertyAttribute>().PropertyName == arrayField[1 + i]);

                                    if (propChild == null)
                                        continue;

                                    fieldName = propChild.GetCustomAttribute<DisplayAttribute>()?.Name ?? propChild?.Name;
                                    break;
                                }
                            }
                            else
                            {
                                fieldName = propField.GetCustomAttribute<DisplayAttribute>()?.Name ?? propField?.Name;
                            }

                            if (string.IsNullOrEmpty(fieldName))
                                fieldName = dataError.Errors.FirstOrDefault().Key;
                        }
                        catch (Exception)
                        {
                            if (string.IsNullOrEmpty(fieldName))
                                fieldName = dataError.Errors.FirstOrDefault().Key;
                        }

                        #endregion
                        firstMessage = $"{fieldName} {dataError.Errors.FirstOrDefault().Value.FirstOrDefault()}".Trim();

                        if (string.IsNullOrEmpty(firstMessage) == false)
                            firstMessage = Regex.Replace(firstMessage, "^base", "").Trim();

                    }

                    var allErrors = dataError.Errors;

                    baseErrors.Error = allErrors;
                    baseErrors.MessageError = firstMessage;
                    baseErrors.HasError = true;
                    break;

                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:

                    try
                    {
                        baseErrors.MessageError = response.Content.Contains("[") && response.Content.Contains(":") ? JsonConvert.DeserializeObject<IuguErrorsArray>(response.Content)?.Errors[0] : JsonConvert.DeserializeObject<IuguError>(response.Content)?.Errors;
                    }
                    catch (Exception)
                    {
                        baseErrors.MessageError = response.Content;
                    }
                    baseErrors.HasError = true;
                    break;

                case 0:
                    baseErrors.MessageError = "Não foi possivel estabelecer uma conexão com o servidor";
                    baseErrors.HasError = true;
                    break;
            }

            if (showContent)
                baseErrors.Content = response.Content;

            return baseErrors;
        }

        /// <summary>
        /// VERIFICA SE É UM LR DE SUCESSO
        /// </summary>
        /// <param name="LR"></param>
        /// <returns></returns>
        public static bool SuccessTransaction(this string LR)
        {
            return (LR == "00" || LR == "000" || LR == "11");
        }

        /// <summary>
        /// MAPEAR ERROS RETORNADOS PELA IUGU
        /// </summary>
        /// <param name="response"></param>
        /// <returns></returns>
        public static IuguBaseErrors IuguMapErrors(this IRestResponse response)
        {
            var baseErrors = new IuguBaseErrors();

            switch (response.StatusCode)
            {
                case (HttpStatusCode)422:
                    var dataError = JsonConvert.DeserializeObject<IuguErrors422>(response.Content);

                    var firstMessage = $"{dataError.Errors.FirstOrDefault().Key} {dataError.Errors.FirstOrDefault().Value}".Trim();

                    if (string.IsNullOrEmpty(firstMessage) == false)
                        firstMessage = Regex.Replace(firstMessage, "^base", "").Trim();

                    var allErrors = dataError.Errors;

                    baseErrors.Error = allErrors;
                    baseErrors.MessageError = firstMessage;
                    baseErrors.HasError = true;
                    break;
                case HttpStatusCode.BadRequest:
                case HttpStatusCode.Unauthorized:
                case HttpStatusCode.NotFound:

                    baseErrors.MessageError = JsonConvert.DeserializeObject<IuguError>(response.Content)?.Errors;
                    baseErrors.HasError = true;
                    break;
                case 0:

                    baseErrors.MessageError = "Não foi possivel estabelecer uma conexão com o servidor";
                    baseErrors.HasError = true;
                    break;
            }
            return new IuguBaseErrors();
        }

        /// <summary>
        /// valida dados bancarios
        /// </summary>
        /// <param name="conta"></param>
        /// <param name="banco">BANCO = NOME</param>
        /// <param name="typeAccount">TIPO DE CONTA</param>
        /// <param name="typePerson">Pessoa Física / Pessoa Júridica</param>
        /// <returns></returns>
        [Obsolete]
        public static string ValidAccoutBank(this string conta, string banco, string typeAccount, string typePerson = "Pessoa Física")
        {
            if (string.IsNullOrEmpty(conta))
                return conta;

            conta = conta.RemoveOperacao(banco);

            switch (banco.ToLower())
            {
                case "via credi":
                case "085":
                    // LENGTH 11
                    return conta.MaskAccountBank(@"00000000000\-0", 11);
                case "nubank":
                case "260":
                    // LENGTH 10
                    return conta.MaskAccountBank(@"0000000000\-0", 10);
                case "banrisul":
                case "041":
                case "sicoob":
                case "756":
                case "inter":
                case "077":
                case "brb":
                case "070":
                case "modal":
                case "746":
                    // LENGTH 9
                    return conta.MaskAccountBank(@"0000000000\-0", 9);
                case "banco do brasil":
                case "001":
                case "santander":
                case "033":
                case "caixa econômica":
                case "104":
                case "pagseguro":
                case "290":
                case "safra":
                case "422":
                case "banestes":
                case "021":
                case "unicred":
                case "136":
                case "gerencianet pagamentos do brasil":
                case "364":
                    // LENGTH 8
                    conta = conta.MaskAccountBank(@"00000000\-0", 8);
                    if (typeAccount.Equals("Poupança") && (banco.Equals("Caixa Econômica") || banco.Equals("104")) && conta.Length > 9)
                        conta = $"013{conta}";

                    if (typeAccount.Equals("Corrente") && (banco.Equals("Caixa Econômica") || banco.Equals("104")) && conta.Length > 9)
                        conta = $"001{conta}";

                    return conta;
                case "bradesco":
                case "237":
                case "neon":
                case "votorantim":
                case "655":
                case "banco original":
                case "212":
                    // LENGTH 7
                    return conta.MaskAccountBank(@"0000000\-0", 7);
                case "itaú":
                case "341":
                    // LENGTH 5
                    return conta.MaskAccountBank(@"00000\-0", 5);
                case "sicredi":
                case "748":
                    // LENGTH 5 SEM HIFEN
                    return conta.MaskAccountBank(@"000000", 5, false);
                default:
                    // LENGTH 9
                    return conta.MaskAccountBank(@"000000000\-0", 9);
            }
        }

        /// <summary>
        /// COLOCAR MASK DE CONTA BANCÁRIA
        /// </summary>
        /// <param name="account"></param>
        /// <param name="mask"></param>
        /// <param name="minLength"></param>
        /// <param name="hasHyphen"></param>
        /// <returns></returns>
        [Obsolete]
        public static string MaskAccountBank(this string account, string mask, int minLength, bool hasHyphen = true)
        {
            var accountAndDigit = account.Split('-');
            try
            {
                if (account.IndexOf("-", StringComparison.Ordinal) == -1)
                {
                    //CASO  NÃO TENHA O DIGITO
                    account = Convert.ToUInt64(account).ToString(mask);
                    accountAndDigit = account.Split('-');
                    account = accountAndDigit[0];
                }
                else
                {
                    account = accountAndDigit[0];
                }

                if (account.Length > minLength)
                {
                    account = account.Substring(0, minLength);
                }
                else
                {
                    while (account.Length < minLength)
                    {
                        account = $"0{account.Trim()}";
                    }
                }
                var response = accountAndDigit.Length > 1 ? $"{account}-{accountAndDigit[1]}" : $"{account}-0";

                return hasHyphen ? response?.TrimSpaces() : response?.OnlyNumbers()?.Trim();
            }
            catch (Exception)
            {

                return account;
            }
        }

        /// <summary>
        /// VALIDAR AGENCIA
        /// </summary>
        /// <param name="agencia"></param>
        /// <param name="banco"></param>
        /// <returns></returns>
        [Obsolete]
        public static string ValidAgencyBank(this string agencia, string banco)
        {
            if (string.IsNullOrEmpty(agencia))
                return agencia;

            switch (banco.ToLower())
            {
                case "banco do brasil":
                case "bradesco":
                case "001": //BANCO DO BRASIL
                case "237": //BRADESCO

                    var dataAgency = agencia.Split('-');

                    if (dataAgency == null || dataAgency.Length == 0)
                        return agencia;

                    var agency = dataAgency[0];
                    var agencyDigit = dataAgency.Length > 1 && string.IsNullOrEmpty(dataAgency[1]) == false ? dataAgency[1] : "0";

                    return $"{agency}-{agencyDigit}".TrimSpaces();
                default: //SEM DIGITO
                    return agencia.OnlyNumbers()?.TrimSpaces();
            }
        }
        /// <summary>
        /// OBTER CÓDIGO DO BANCO
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        [Obsolete]
        public static string GetCodeBank(this string bank)
        {
            if (string.IsNullOrEmpty(bank))
                return bank;

            switch (bank.ToLower())
            {
                case "bradesco":
                    return "237";
                case "banco do brasil":
                    return "001";
                case "santander":
                    return "033";
                case "caixa econômica":
                    return "104";
                case "sicredi":
                    return "748";
                case "sicoob":
                    return "756";
                case "brb":
                    return "070";
                case "inter":
                    return "077";
                case "banrisul":
                    return "041";
                case "safra":
                    return "422";
                case "banco original":
                    return "212";
                case "pagseguro":
                    return "290";
                case "nubank":
                    return "260";
                case "neon":
                    return "655";
                case "via credi":
                    return "085";
                case "votorantim":
                    return "655";
                case "modal":
                    return "746";
                case "banestes":
                    return "021";
                case "unicred":
                    return "136";
                case "gerencianet pagamentos":
                    return "364";
                case "itaú":
                    return "341";
                default:
                    return bank;
            }
        }

        /// <summary>
        /// ONLY USE UPDATE DATA BANK
        /// </summary>
        /// <param name="typeAccount"></param>
        /// <returns></returns>
        public static string GetTypeAccout(this string typeAccount)
        {
            if (string.IsNullOrEmpty(typeAccount))
                return typeAccount;

            return typeAccount.ToLower() switch
            {
                "corrente" => "cc",
                _ => "cp",
            };
        }

        /// <summary>
        /// RETORNA MENSAGEM DE ERRO DE ACORDO COM CÓDIGO DA IUGU
        /// </summary>
        /// <param name="codeErro"></param>
        /// <returns></returns>
        public static string STATUS_LR(string codeErro)
        {
            switch (codeErro)
            {
                case "1":
                case "01":
                    return "Transação não autorizada. Referida (suspeita de fraude) pelo banco emissor.";
                case "2":
                case "02":
                    return "Transação não autorizada. Referida (suspeita de fraude) pelo banco emissor.";
                case "3":
                case "03":
                    return "Não foi possível processar a transação.";
                case "4":
                case "04":
                    return "Transação não autorizada. Cartão bloqueado pelo banco emissor.";
                case "5":
                case "05":
                    return "Transação não autorizada. Cartão inadimplente (Do not honor).";
                case "6":
                case "06":
                    return "Transação não autorizada. Cartão cancelado.";
                case "7":
                case "07":
                    return "Transação negada.";
                case "8":
                case "08":
                    return "Transação não autorizada. Código de segurança inválido.";
                case "9":
                case "09":
                    return "Transação cancelada parcialmente com sucesso.";
                case "12": return "Transação inválida, erro no cartão.";
                case "13": return "Transação não permitida. Valor da transação Inválido.";
                case "14": return "Transação não autorizada. Cartão Inválido";
                case "15": return "Banco emissor indisponível ou inexistente.";
                case "19": return "-";
                case "21": return "Cancelamento não efetuado. Transação não localizada.";
                case "22": return "Parcelamento inválido. Número de parcelas inválidas.";
                case "23": return "Transação não autorizada. Valor da prestação inválido.";
                case "24": return "Quantidade de parcelas inválido.";
                case "25": return "Pedido de autorização não enviou número do cartão";
                case "28": return "Arquivo temporariamente indisponível.";
                case "30": return "Transação não autorizada. Decline Message";
                case "39": return "Transação não autorizada. Erro no banco emissor.";
                case "41": return "Transação não autorizada. Cartão bloqueado por perda.";
                case "43": return "Transação não autorizada. Cartão bloqueado por roubo.";
                case "51": return "Transação não autorizada. Limite excedido/sem saldo.";
                case "52": return "Cartão com dígito de controle inválido.";
                case "53": return "Transação não permitida. Cartão poupança inválido";
                case "54": return "Transação não autorizada. Cartão vencido";
                case "55": return "Transação não autorizada. Senha inválida";
                case "56": return "Número cartão não pertence ao emissor | Número cartão inválido";
                case "57": return "Transação não permitida para o cartão";
                case "58": return "Transação não permitida. Opção de pagamento inválida.";
                case "59": return "Transação não autorizada. Suspeita de fraude.";
                case "60":
                case "79":
                case "FA": return "Transação não autorizada.";
                case "61": return "Banco emissor indisponível.";
                case "62": return "Transação não autorizada. Cartão restrito para uso doméstico";
                case "63": return "Transação não autorizada. Violação de segurança";
                case "64": return "Transação não autorizada. Valor abaixo do mínimo exigido pelo banco emissor.";
                case "65": return "Transação não autorizada. Excedida a quantidade de transações para o cartão.";
                case "67": return "Transação não autorizada. Cartão bloqueado para compras hoje.";
                case "70": return "Transação não autorizada. Limite excedido/sem saldo.";
                case "72": return "Cancelamento não efetuado. Saldo disponível para cancelamento insuficiente.";
                case "74": return "Transação não autorizada. A senha está vencida.";
                case "75": return "Senha bloqueada. Excedeu tentativas de cartão.";
                case "76": return "Cancelamento não efetuado. Banco emissor não localizou a transação original";
                case "77": return "Cancelamento não efetuado. Não foi localizado a transação original";
                case "78": return "Transação não autorizada. Cartão bloqueado primeiro uso.";
                case "80": return "Transação não autorizada. Divergencia na data de transação/pagamento.";
                case "81": return "Transação não autorizada. A senha está vencida.";
                case "82": return "Transação não autorizada. Cartão inválido.";
                case "83": return "Transação não autorizada. Erro no controle de senhas";
                case "85":
                case "86":
                case "90":
                case "AG":
                case "AF":
                case "BD":
                case "BO": return "Transação não permitida. Falha da operação.";
                case "88": return "Falha na criptografia dos dados.";
                case "89": return "Erro na transação.";
                case "91": return "Transação não autorizada. Banco emissor temporariamente indisponível.";
                case "92": return "Transação não autorizada. Tempo de comunicação excedido.";
                case "93": return "Transação não autorizada. Violação de regra, possível erro no cadastro.";
                case "94": return "Transação duplicada.";
                case "96": return "Falha no processamento.";
                case "97": return "Valor não permitido para essa transação.";
                case "98":
                case "99":
                case "999": return "Sistema/comunicação indisponível.";
                case "475": return "Timeout de Cancelamento";
                case "A2": return "Verifique os dados do cartão";
                case "A3":
                case "A7": return "Erro no cartão";
                case "A5": return "Transação não permitida";
                case "AA": return "Tempo Excedido";
                case "AB": return "Função incorreta (débito)";
                case "AC": return "Transação não permitida. Cartão de débito sendo usado com crédito.";
                case "AE": return "Tente Mais Tarde";
                case "AH": return "Transação não permitida. Cartão de crédito sendo usado com débito.";
                case "AI": return "Transação não autorizada. Autenticação não foi realizada.";
                case "AJ": return "Transação não permitida. Transação de crédito ou débito em uma operação que permite apenas Private Label.";
                case "AV": return "Transação não autorizada. Dados Inválidos";
                case "BL": return "Transação não autorizada. Limite diário excedido.";
                case "BM": return "Transação não autorizada. Cartão Inválido";
                case "BN": return "Transação não autorizada. Cartão ou conta bloqueado.";
                case "BP": return "Transação não autorizada. Conta corrente inexistente.";
                case "BP176":
                case "GD":
                case "NR":
                case "RP": return "Transação não permitida.";
                case "BV": return "Transação não autorizada. Cartão vencido";
                case "CF": return "Transação não autorizada.C79: returnJ79 Falha na validação dos dados.";
                case "CG":
                case "DA":
                case "DQ":
                case "KE": return "Transação não autorizada. Falha na validação dos dados.";
                case "DF": return "Transação não permitida. Falha no cartão ou cartão inválido.";
                case "DM": return "Transação não autorizada. Limite excedido/sem saldo.";
                case "DS": return "Transação não permitida para o cartão";
                case "EB": return "Transação não autorizada. Limite diário excedido.";
                case "EE": return "Transação não permitida. Valor da parcela inferior ao mínimo permitido.";
                case "EK": return "Transação não permitida para o cartão";
                case "FC": return "Transação não autorizada. Ligue Emissor";
                case "FD": return "Transação negada. Reter cartão condição especial";
                case "FE": return "Transação não autorizada. Divergencia na data de transação/pagamento.";
                case "FF": return "Cancelamento OK";
                case "FG": return "Transação não autorizada. Ligue AmEx 08007285090.";
                case "GA": return "Aguarde Contato";
                case "HJ": return "Transação não permitida. Código da operação inválido.";
                case "IA": return "Transação não permitida. Indicador da operação inválido.";
                case "JB": return "Transação não permitida. Valor da operação inválido.";
                case "P5": return "Troca de senha / desbloqueio";
                case "KA": return "Transação não permitida. Falha na validação dos dados.";
                case "KB": return "Transação não permitida. Selecionado a opção incorrente.";
                case "N7": return "Transação não autorizada. Código de segurança inválido.";
                case "R0": return "SUSPENSÃO DE PAGAMENTO RECORRENTE PARA UM SERVIÇO";
                case "R1": return "Transação não autorizada. Cartão inadimplente (Do not honor)";
                case "R2": return "Transação não qualificada para visa pin";
                case "R3": return "Suspensão de todas as ordens de autorização";
                case "U3": return "Transação não permitida. Falha na validação dos dados.";
                case "N3": return "Saque não disponível";
                case "N8": return "Diferença. pré autorização";
                case "99A": return "Token não encontrado";
                case "99B": return "Sistema indisponível/Falha na comunicação";
                case "99C": return "Sistema indisponível/Exceção no processamento";
                case "99Z": return "Sistema indisponível/Retorno desconhecido";
                case "99TA": return "Timeout na requisição. O tempo para receber o retorno da requisição excedeu.";
                case "AF01": return "Recusado manualmente em analise antifraude";
                case "AF02": return "Recusado automaticamente em analise antifraude";
                case "AF03": return "Recusado pelo antifraude da adquirente de crédito";
                case "126": return "A data de validade do cartão de crédito é inválida";

            }
            return null;
        }

        /// <summary>
        /// MAPEAR CAMPOS EM CLASS
        /// </summary>
        /// <param name="model"></param>
        /// <param name="form"></param>
        /// <returns></returns>
        public static IuguTriggerModel SetAllProperties(this IuguTriggerModel model, IFormCollection form)

        {
            if (form == null)
                return model;

            model.Data.AccountId = form["data[account_id]"];
            model.Data.SubscriptionId = form["data[subscription_id]"];
            model.Data.Source = form["data[source]"];
            model.Data.OrderId = form["data[order_id]"];
            model.Data.ExternalReference = form["data[external_reference]"];
            model.Data.PaymentMethod = form["data[payment_method]"];
            model.Data.PaidAt = form["data[paid_at]"];
            model.Data.PayerCpfCnpj = form["data[payer_cpf_cnpj]"];
            model.Data.PixEndToEndId = form["data[pix_end_to_end_id]"];
            model.Data.CustomerName = form["data[customer_name]"];
            model.Data.CustomerEmail = form["data[customer_email]"];
            model.Data.ExpiresAt = form["data[expires_at]"];
            model.Data.PlanIdentifier = form["data[plan_identifier]"];
            model.Data.ChargeLimitCents = form["data[charge_limit_cents]"];
            model.Data.OccurrenceDate = form["data[occurrence_date]"];
            model.Data.AccountNumberLastDigits = form["data[account_number_last_digits]"];
            model.Data.Feedback = form["data[feedback]"];
            model.Data.WithdrawRequestId = form["data[withdraw_request_id]"];

            // var amountForm = (string)form["data[amount]"];
            // int.TryParse(form["data[number_of_installments]"], out var numberOfInstallments);
            // int.TryParse(form["data[amount_cents]"], out var amountCent);
            // int.TryParse(form["data[fee_cents]"], out var feeCents);
            // int.TryParse(form["data[paid_cents]"], out var paidCents);
            // int.TryParse(form["data[commission_cents]"], out var commissionCents);
            // int.TryParse(form["data[total]"], out var total);
            // int.TryParse(form["data[taxes]"], out var taxes);
            // int.TryParse(form["data[fines]"], out var fines);
            // int.TryParse(form["data[discount]"], out var discount);
            // int.TryParse(form["data[client_share]"], out var clientShare);
            // int.TryParse(form["data[net_value]"], out var netValue);
            // int.TryParse(form["data[early_payment_discount]"], out var earlyPaymentDiscount);
            // double.TryParse(amountForm?.Replace(",", "").Replace(".", ","), out var amount);

            // model.Data.Amount = amount;
            // model.Data.AmountCents = amountCent;
            // model.Data.NumberOfInstallments = numberOfInstallments;
            // model.Data.FeeCents = feeCents;
            // model.Data.PaidCents = paidCents;
            // model.Data.CommissionCents = commissionCents;
            // model.Data.Total = total;
            // model.Data.Fines = fines;
            // model.Data.Taxes = taxes;
            // model.Data.Discount = discount;
            // model.Data.ClientShare = clientShare;
            // model.Data.NetValue = netValue;
            // model.Data.EarlyPaymentDiscount = earlyPaymentDiscount;

            return model;

        }

        /// <summary>
        /// VERIFICA SE CONTÉM O ERRO DE CONTA JÁ VERIFICADA
        /// </summary>
        /// <param name="dictionary"></param>
        /// <returns></returns>
        public static bool CheckAlreadyVerified(this IDictionary<string, List<string>> dictionary)
        {
            try
            {
                return dictionary.Any(x => x.Value.Any(y => y.Contains("conta já verificada")));
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// ATUALIZAR MASCARA DE CONTA E AGENCIA
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async static Task<DataBankViewModel> SetBankMask(this DataBankViewModel model)
        {
            try
            {
                if (model.Masked || !model.HasDataBank())
                    return model;

                var response = await Utilities.CallApi(new CallApiViewModel()
                {
                    Url = "https://api.mecabr.com/api/v1/Bank/List",
                    Method = Method.GET
                });

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var returnBanks = JsonConvert.DeserializeObject<ReturnGenericViewModel<List<BankInfoViewModel>>>(response.Content);

                    var bankItem = returnBanks.Data.Find(x => x.Code == model.Bank.ToLower() || x.Name.EqualsIgnoreCase(model.Bank));

                    if (bankItem != null)
                    {
                        var pattern = @"(9|D)";

                        model.BankAgency = model.BankAgency.ApplyMask(Regex.Replace(bankItem.AgencyMask, pattern, "0"), true, true);
                        model.BankAccount = model.BankAccount.ApplyMask(Regex.Replace(bankItem.AccountMask, pattern, "0"), true, true);
                        model.Bank = bankItem.Name;
                        model.BankCode = bankItem.Code;

                        if (bankItem.Code == "104")
                            model.BankAccount = model.BankAccount.SetOperation(model.AccountType);

                        model.SetMasked(true);
                    }
                }
                return model;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        /// ADICIONAR OPERAÇÃO CASO NÃO TENHA
        /// </summary>
        /// <param name="account">NÚMERO DA CONTA COM OU SEM OPERAÇÃO</param>
        /// <param name="typeAccount">TIPO DE CONTA (corrente/poupança)</param>
        /// <returns></returns>
        public static string SetOperation(this string account, string typeAccount)
        {
            var operation = "";
            try
            {
                if (account.Length <= 10)
                    operation = typeAccount.EqualsIgnoreCase("corrente") ? "001" : "013";
            }
            catch (Exception) {/*unused*/}

            return operation + account;
        }

        /// <summary>
        /// INCLUIR CONTENT NO RETORNO
        /// </summary>
        /// <param name="response"></param>
        /// <param name="showContent"></param>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public static string ShowContent<TEntity>(this IRestResponse<TEntity> response, bool showContent) where TEntity : IuguBaseErrors
        {
            string content = null;
            try
            {
                if (showContent)
                    content = response.Content;
            }
            catch (Exception) {/*UNUSED*/}
            return content;
        }

        /// <summary>
        /// ADICIONAR TAXAS TRANSACIONAIS
        /// </summary>
        /// <param name="value"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static double AddTransactionalFee(this double value, double percent)
            => value / (1 - (percent / 100));
        /// <summary>
        /// OBTER VALOR DAS TAXAS TRANSACIONAIS
        /// </summary>
        /// <param name="value"></param>
        /// <param name="percent"></param>
        /// <returns></returns>
        public static double GetTransactionalFee(this double value, double percent)
            => (value / (1 - (percent / 100))) - value;

    }
}