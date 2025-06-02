using System;
using Stripe;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripePaymentIntentDetails
    {
        /// <summary>
        /// FORMA DE PAGAMENTO UTILIZADA
        /// </summary>
        /// <value></value>
        public string PaymentMethodType { get; set; }
        /// <summary>
        /// DETALHES DA FORMA DE PAGAMENTO
        /// </summary>
        /// <value></value>
        public string PaymentMethodDetails { get; set; }
        /// <summary>
        /// URL DO RECIBO DE PAGAMENTO
        /// </summary>
        /// <value></value>
        public string ReceiptUrl { get; set; }
        /// <summary>
        /// VALOR BRUTO PAGO
        /// </summary>
        /// <value></value>
        public double GrossAmount { get; set; }
        /// <summary>
        /// VALOR LIQUIDO
        /// </summary>
        /// <value></value>
        public double NetAmount { get; set; }
        /// <summary>
        /// TAXAS DE PROCESSAMENTO
        /// </summary>
        /// <value></value>
        public double ProcessingFee { get; set; }
        /// <summary>
        /// STATUS DA LIBERAÇÃO DO VALOR PAGO
        /// </summary>
        /// <value></value>
        public string FundsStatus { get; set; }
        /// <summary>
        /// MOEDA
        /// </summary>
        /// <value></value>
        public string Currency { get; set; }
        /// <summary>
        /// PREVISÃO DE LIBERAÇÃO DO VALOR PAGO
        /// </summary>
        /// <value></value>
        public DateTime? AvailableOn { get; set; }
        /// <summary>
        /// VALOR COBRADO
        /// </summary>
        /// <value></value>
        public double? AmountCaptured { get; set; }
        /// <summary>
        /// VALOR ESTORNADO
        /// </summary>
        /// <value></value>
        public double? RefundedAmount { get; set; }
        /// <summary>
        /// IDENTIFICADOR DO SALDO DA COBRANÇA
        /// </summary>
        /// <value></value>
        public string BalanceTransactionId { get; set; }
        /// <summary>
        /// IDENTIFICADOR DA COBRANÇA
        /// </summary>
        /// <value></value>
        public string ChargeId { get; set; }
        /// <summary>
        /// SPLIT VINCULADO
        /// </summary>
        /// <value></value>
        public AnyOf<ChargeTransferData, PaymentIntentTransferData> TransferData { get; set; }
        /// <summary>
        /// STATUS DO PAGAMENTO
        /// </summary>
        /// <value></value>
        public EStripePaymentStatus PaymentStatus { get; set; }
        /// <summary>
        /// SCORE DE RISCO
        /// </summary>
        /// <value></value>
        public long? RiskScore { get; set; }
        /// <summary>
        /// NÍVEL DE RISCO
        /// </summary>
        /// <value></value>
        public string RiskLevel { get; set; }
        /// <summary>
        /// DETALHES DO RISCO
        /// </summary>
        /// <value></value>
        public string RiskDetails { get; set; }
        /// <summary>
        /// DADOS DA FORMA DE PAGAMENTO
        /// </summary>
        /// <value></value>
        public ChargePaymentMethodDetails PaymentMethod { get; set; }
    }
}