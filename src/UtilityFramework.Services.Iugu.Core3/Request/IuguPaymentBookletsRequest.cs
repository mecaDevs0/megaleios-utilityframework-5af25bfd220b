using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Request
{

    public class IuguPaymentBookletsRequest
    {
        /// <summary>
        /// Detalhes dos desconto de pagamento antecipado
        /// </summary>
        [JsonProperty("early_payment_discounts")]
        public List<EarlyPaymentDiscountsModel> EarlyPaymentDiscounts { get; set; } = new List<EarlyPaymentDiscountsModel>();
        /// <summary>
        /// (required) ID do cliente na iugu que você deseja criar o carnê
        /// </summary>
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }
        /// <summary>
        /// (required) Descrição do carnê
        /// </summary>
        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// (required) Quantidade de parcelas presentes no carnê. Deve ser escolhido entre 1-24 parcelas.
        /// </summary>
        [JsonProperty("installments")]
        public int Installments { get; set; }
        /// <summary>
        /// Data de início da cobrança do carnê. Se não enviado, o início será imediato. YYYY-MM-DD
        /// </summary>
        [JsonProperty("started_at")]
        public string StartedAt { get; set; }
        /// <summary>
        /// Habilita multa por atraso de pagamento
        /// </summary>
        [JsonProperty("invoice_fines")]
        public bool InvoiceFines { get; set; }
        /// <summary>
        /// Determine a % da multa a ser cobrada para pagamentos efetuados após a data de vencimento
        /// </summary>
        [JsonProperty("invoice_fines_value")]
        public string InvoiceFinesValue { get; set; }
        /// <summary>
        /// Booleano que determina se cobra ou não juros por dia de atraso. Necessário passar a multa como true
        /// </summary>
        [JsonProperty("invoice_per_day_interest")]
        public bool InvoicePerDayInterest { get; set; }
        /// <summary>
        /// Informar o valor percentual de juros por atraso que deseja cobrar
        /// </summary>
        [JsonProperty("invoice_per_day_interest_value")]
        public string InvoicePerDayInterestValue { get; set; }
        /// <summary>
        /// (required) Valor total do carnê em centavos. Mínimo de 1$ real (100) por parcela.
        /// </summary>
        [JsonProperty("price_cents")]
        public int PriceCents { get; set; }
        /// <summary>
        /// Envie 'true' para gerar o Carnê com o método PIX habilitado (QR code). Caso não envie este campo (ou envie 'false'), o carnê será gerado apenas com os boletos bancários.
        /// </summary>
        [JsonProperty("invoice_payable_with_pix")]
        public bool InvoicePayableWithPix { get; set; }
        /// <summary>
        /// Ativa ou desativa os descontos por pagamento antecipado. Quando true, sobrepõe as configurações de desconto da conta.
        /// </summary>
        [JsonProperty("invoice_early_payment_discount")]
        public bool InvoiceEarlyPaymentDiscount { get; set; }
        /// <summary>
        /// Referência externa do carnê
        /// </summary>
        [JsonProperty("external_reference")]
        public string ExternalReference { get; set; }
        /// <summary>
        /// Se true, o cliente final não irá receber nenhum e-mail notificando a cobrança.
        /// </summary>
        [JsonProperty("ignore_due_email")]
        public bool IgnoreDueEmail { get; set; }
    }
}