using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using RestSharp;
using UtilityFramework.Application.Core;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core
{
    public static class ZoopUtilities
    {
        public static Dictionary<string, string> ChangeMarketPlace(this string marketPlaceId, Dictionary<string, string> tags, string _marketplaceId = null)
        {
            tags.Clear();
            tags["{{marketplace_id}}"] = marketPlaceId ?? _marketplaceId;

            return tags;
        }

        public static RestRequest MapRequestParameter<T>(this RestRequest request, T entity)
        {
            try
            {
                var properties = entity.GetType().GetProperties().ToList();

                request.AddHeader("Content-Type", "multipart/form-data");
                foreach (var item in properties)
                {
                    var propName = $"{((JsonPropertyAttribute)item.GetCustomAttributes(typeof(JsonPropertyAttribute), false)?.FirstOrDefault())?.PropertyName ?? item.Name}";

                    if (item.PropertyType != typeof(IFormFile))
                    {
                        request.AddParameter(propName, Utilities.GetValueByProperty(entity, item.Name));
                    }
                    else
                    {
                        request.AlwaysMultipartFormData = true;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return request;
        }
        public static RestRequest MapQueryString<T>(this RestRequest request, T entity, ParameterType parameterType = ParameterType.QueryString)
        {
            try
            {
                var properties = entity.GetType().GetProperties().ToList();

                foreach (var item in properties)
                {
                    var value = item.GetValue(entity, null);
                    if (Equals(value, null) == false)
                    {
                        var propName = $"{((JsonPropertyAttribute)item.GetCustomAttributes(typeof(JsonPropertyAttribute), false)?.FirstOrDefault())?.PropertyName ?? item.Name}";

                        request.AddParameter(propName, value, parameterType);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return request;
        }

        public static string MessageByCategoryError(this CategoryError categoryError)
        {

            try
            {
                switch (categoryError)
                {
                    case CategoryError.ServerApiError:
                        return "Ocorreu um erro ao processar a requisição, entre em contato com suporte técnico.";
                    case CategoryError.DuplicateTaxpayerId:
                        return "Esse número de documento já está registrado no sistema.";
                    case CategoryError.RequestTimeout:
                        return "O servidor não respondeu a essa solicitação, tente novamente. Se o problema persistir, entre em contato com o suporte técnico.";
                    case CategoryError.NotFound:
                        return "O URL solicitado não foi encontrado no servidor";
                    case CategoryError.UnAuthorized:
                        return "As credenciais do usuário fornecidas falharam na validação do serviço solicitado";
                    case CategoryError.ExpiredSecurityKey:
                        return "A chave da API fornecida expirou ou foi excluída.";
                    case CategoryError.InvalidKey:
                        return "Essa chamada de API não pode ser feita com uma chave de API publicável.";
                    case CategoryError.TransactionAmountError:
                        return "O valor mínimo é de R$0,50";
                    case CategoryError.TransferAmountError:
                        return "O valor mínimo de transferência é de R$ 1,00";
                    case CategoryError.MissingRequiredParam:
                        return "Parâmetro(s) requerido(s) ausente(s). Por favor, verifique os parâmetros da solicitação.";
                    case CategoryError.UnSupportedPaymentType:
                        return "Pedido inválido: tipo de pagamento não suportado.";
                    case CategoryError.InvalidPaymentInformation:
                        return "Informações de pagamento inválidas. Por favor, verifique os parâmetros da solicitação.";
                    case CategoryError.InvalidParameter:
                        return "Por favor, verifique os parâmetros da solicitação.";
                    case CategoryError.FileLarge:
                        return "Arquivo muito grande.";
                    case CategoryError.InsufficientEscrowFunds:
                        return "A transferência solicitada excede o saldo disponível.";
                    case CategoryError.CaptureTransactionError:
                        return "A solicitação de captura falhou. A transação não pôde ser capturada.";
                    case CategoryError.NoActionTaken:
                        return "Nenhuma ação tomada. Não é possível fazer o backup da transação anterior";
                    case CategoryError.SellerAuthorizationRefused:
                        return "O vendedor não foi autorizado a cobrar cartões de crédito. Ativação completa para iniciar o processamento de pagamentos.";
                    case CategoryError.VoidTransactionError:
                        return "A solicitação vazia falhou. A transação não pode ser anulada.";
                    case CategoryError.InvalidExpiryMonth:
                        return "Valor do mês de expiração inválido.";
                    case CategoryError.InvalidExpiryYear:
                        return "Valor do ano de expiração inválido.";
                    case CategoryError.CardCustomerNotAssociated:
                        return "Transação negada. Nenhum cartão ativo.";
                    case CategoryError.InsufficientFundsError:
                        return "O crédito requerido excede o saldo disponível.";
                    case CategoryError.ExpiredCardError:
                        return "O cartão de crédito expirou.";
                    case CategoryError.InvalidCardNumber:
                        return "O número do cartão não é um número de cartão de crédito válido.";
                    case CategoryError.InvalidPinCode:
                        return "Transação negada. Código PIN inválido.";
                    case CategoryError.AuthorizationRefused:
                        return "Transação inválida";
                    default:
                        return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}