using System.Collections.Generic;
using System.Threading.Tasks;
using UtilityFramework.Services.Iugu.Core3.Entity;
using UtilityFramework.Services.Iugu.Core3.Entity.Lists;
using UtilityFramework.Services.Iugu.Core3.Models;
using UtilityFramework.Services.Iugu.Core3.Request;
using UtilityFramework.Services.Iugu.Core3.Response;

namespace UtilityFramework.Services.Iugu.Core3.Interface
{
    /// <summary>
    /// TRANSAÇÕES IUGU
    /// </summary>
    public interface IIuguChargeServices
    {
        /// <summary>
        /// COBRANÇA NO CARTÃO DE CRÉDITO
        /// </summary>
        /// <param name="model"></param>
        /// <param name="clienteId"></param>
        /// <param name="idCartao"></param>
        /// <param name="token"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguChargeResponse> TransacaoCreditCardAsync(IuguChargeRequest model, string clienteId, string idCartao = null, string token = null, string apiToken = null);
        /// <summary>
        /// OBTER DETALHES DE UMA FATURA
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguInvoiceResponseMessage> GetFaturaAsync(string invoiceId, string apiToken = null);
        /// <summary>
        /// ESTORNAR VALOR COBRADO DE UMA FATURA OU PARCIAL
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="refundCents"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguChargeResponse> EstornarFaturaAsync(string invoiceId, int? refundCents = null, string apiToken = null);
        /// <summary>
        /// GERAR UMA ASSINATURA
        /// </summary>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<SubscriptionResponseMessage> GerarAssinaturaAsync(SubscriptionRequestMessage model, string apiToken = null);
        /// <summary>
        /// OBTER DADOS DA ASSINATURA
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<SubscriptionResponseMessage> BuscarAssinaturaAsync(string subscriptionId, string apiToken = null);
        /// <summary>
        /// REMOVER ASSINATURA
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<SubscriptionResponseMessage> RemoverAssinaturaAsync(string subscriptionId, string apiToken = null);
        /// <summary>
        /// ALTERAR DADOS DE UM PLANO PARA ASSINATURA
        /// </summary>
        /// <param name="subscriptionId"></param>
        /// <param name="planIdentifier"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<SubscriptionResponseMessage> AlterarPlanoDaAssinaturaAsync(string subscriptionId, string planIdentifier, string apiToken = null);
        /// <summary>
        /// ATIVAR UMA ASSINATURA
        /// </summary>
        /// <param name="signatureId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<SubscriptionResponseMessage> AtivarAsinaturaAsync(string signatureId, string apiToken = null);
        /// <summary>
        /// SUSPENDER UMA ASSINATURA
        /// </summary>
        /// <param name="signatureId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<SubscriptionResponseMessage> SuspenderAssinaturaAsync(string signatureId, string apiToken = null);
        /// <summary>
        /// CRIAR PLANO PARA ASSINATURA
        /// </summary>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguPlanModel> CriarPlanoAsync(IuguPlanModel model, string apiToken = null);
        /// <summary>
        /// ATUALIZAR PLANO PARA ASSINATURA
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguPlanModel> UpdatePlanoAsync(string planId, IuguPlanModel model, string apiToken = null);
        /// <summary>
        /// REMOVER PLANO DE ASSINATURA
        /// </summary>
        /// <param name="planId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task RemoverPlanoAsync(string planId, string apiToken = null);
        /// <summary>
        /// OBTER RECEBIVEIS
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="limit"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<ListIuguAdvanceResponse> GetRecebiveisAsync(int? skip = null, int? limit = null, string apiToken = null);
        /// <summary>
        /// SIMULAR ANTECIPAÇÃO DE VALORES
        /// </summary>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguAdvanceSimulationResponse> SimularAntecipacaoAsync(IuguAdvanceRequest model, string apiToken = null);
        /// <summary>
        /// SOLICITAR ANTECIPAÇÃO DE VALORES
        /// </summary>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguAdvanceSimulationResponse> SolicitarAntecipacaoAsync(IuguAdvanceRequest model, string apiToken = null);
        /// <summary>
        /// CANCELAR FATURA
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguInvoiceResponseMessage> CancelarFaturaAsync(string invoiceId, string apiToken = null);
        /// <summary>
        /// GERAR FATURA (BOLETO OU CARTÃO)
        /// </summary>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguInvoiceResponseMessage> GerarFaturaAsync(InvoiceRequestMessage model, string apiToken = null);
        /// <summary>
        /// OBTER LOGS DE WEBHOOK
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<List<WebHookViewModel>> GetWebhookLogAsync(string invoiceId, string apiToken = null);
        /// <summary>
        /// REENVIAR WEBHOOK
        /// </summary>
        /// <param name="webhookId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<WebHookViewModel> ResendWebhookAsync(string webhookId, string apiToken = null);
        /// <summary>
        /// ENVIAR SEGUNDA VIA DE FATURA
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="model"></param>
        /// <param name="apiToken"></param>
        /// <returns></returns>
        Task<IuguInvoiceResponseMessage> SendDuplicateAsync(string invoiceId, DuplicateViewModel model, string apiToken = null);
        /// <summary>
        /// ENVIA A FATURA IUGU PARA O EMAIL VINCULADO A ELA
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguInvoiceResponseMessage> ResendInvoiceEmailAsync(string invoiceId, string apiToken = null);
        /// <summary>
        /// CRIAR PEDIDO DE PAGAMENTO DE UMA FATURA IUGU (OBRIGATÓRIO ASSINAR REQUEST)
        /// </summary>
        /// <param name="model"></param>
        /// <param "apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguRequestPaymentResponseViewModel> CreatePaymentRequestAsync(IuguRequestPaymentViewModel model, string apiToken = null);
        /// <summary>
        /// METODO PARA VALIDAR PAGAMENTO EXTERNO
        /// </summary>
        /// <param name="model"></param>
        /// <param name="apiToken">live key da subconta (opcional) uso padrão da conta master</param>
        /// <returns></returns>
        Task<IuguValidadeRequestPaymentResponseViewModel> ValidatePaymentRequestAsync(ValidateRequestPayment model, string apiToken = null);
        /// <summary>
        /// METODO PARA CADASTRO DE CARNÊ (BOLETO / PIX)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IuguPaymentBookletResponse> CreatePaymentBookletAsync(IuguPaymentBookletsRequest model, string apiToken = null);
        /// <summary>
        /// METODO PARA BUSCAR CARNÊ (BOLETO / PIX)
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IuguPaymentBookletResponse> GetPaymentBookletById(string id, string apiToken = null);
        /// <summary>
        /// METODO PARA LISTAR CARNÊ (BOLETO / PIX)
        /// </summary>
        /// <returns></returns>
        Task<PaymentBookletsModel> GetPaymentBooklets(string apiToken = null);

    }

}
