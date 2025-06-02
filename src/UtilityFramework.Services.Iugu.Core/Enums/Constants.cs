namespace UtilityFramework.Services.Iugu.Core.Enums
{
    public static class Constants
    {
        /// <summary>
        /// Forma de pagamento cartão ou boleto
        /// </summary>
        public static class PaymentMethod
        {
            public const string ALL = "all";
            public const string CREDIT_CARD = "credit_card";
            public const string BANK_SLIP = "bank_slip";
            public const string PIX = "pix";
        }

        /// <summary>
        /// Status da fatura ex: pago, draft, pendente, cancelado, etc.
        /// </summary>
        public static class InvoiceStatus
        {
            public const string DRAFT = "draft";
            public const string PENDING = "pending";
            public const string CANCELED = "canceled";
            public const string PAID = "paid";
            public const string PARTIALLY_PAID = "partially_paid";
            public const string REFUNDED = "refunded";
            public const string EXPIRED = "Expired";
        }

        /// <summary>
        /// Os tipos de conta bancária puportados
        /// </summary>
        public static class BankAccountType
        {
            public const string SAVING_ACCOUNT = "Poupança";
            public const string CHECKING_ACCOUNT = "Corrente";
            public const string SAVING_ACCOUNT_ABBREVIATION = "cp";
            public const string CHECKING_ACCOUNT_ABBREVIATION = "cc";
        }

        /// <summary>
        /// Os tipos de pessoa puportados
        /// </summary>
        public static class PersonType
        {
            public const string CORPORATE_ENTITY = "Pessoa Jurídica";
            public const string INDIVIDUAL_PERSON = "Pessoa Física";
        }

        /// <summary>
        /// Os bancos suportados
        /// </summary>
        public static class SupportedBanks
        {
            public const string ITAU = "Itaú";
            public const string BRADESCO = "Bradesco";
            public const string CAIXA_ECONOMICA = "Caixa Econômica";
            public const string BANCO_DO_BRASIL = "Banco do Brasil";
            public const string SANTANDER = "Santander";
        }

        /// <summary>
        /// Os tipos de periodicidade e/ou recorrência
        /// </summary>
        public static class GenerateCycleType
        {
            public const string MONTHLY = "months";
            public const string WEEKLY = "weeks";
        }

        /// <summary>
        /// Modedas disponíveis
        /// </summary>
        public static class CurrencyTypes
        {
            public const string BRL = "BRL";
        }

        /// <summary>
        /// Símbolo de modedas disponíveis
        /// </summary>
        public static class CurrencySymbol
        {
            public const string BRL = "R$";
        }

        public class TriggerEvents
        {
            /// <summary>
            /// ALL EVENTS
            /// </summary>
            public const string All = "all";
            /// <summary>
            /// FATURA - CRIADA
            /// </summary>
            public const string InvoiceCreated = "invoice.created";
            /// <summary>
            /// FATURA - STATUS ALTERADO
            /// </summary>
            public const string InvoiceStatusChanged = "invoice.status_changed";
            /// <summary>
            /// FATURA - ESTORNADA
            /// </summary>
            public const string InvoiceRefund = "invoice.refund";
            /// <summary>
            /// FATURA - PAGAMENTO RECUSADO
            /// </summary>
            public const string InvoicePaymentFailed = "invoice.payment_failed";
            /// <summary>
            /// FATURA -
            /// </summary>
            public const string InvoiceDunningAction = "invoice.dunning_action";
            /// <summary>
            /// FATURA VENCIDA
            /// </summary>
            public const string InvoiceDue = "invoice.due";
            /// <summary>
            /// FATURA - PARCELA LIBERADA
            /// </summary>
            public const string InvoiceInstallmentReleased = "invoice.installment_released";
            /// <summary>
            /// FATURA - LIBERADA
            /// </summary>
            public const string InvoiceReleased = "invoice.released";
            /// <summary>
            /// ASSINATURA - SUSPENSA
            /// </summary>
            public const string SubscriptionSuspended = "subscription.suspended";
            /// <summary>
            /// ASSINATURA - ATIVADA
            /// </summary>
            public const string SubscriptionActived = "subscription.activated";
            /// <summary>
            /// ASSINATURA - CRIADA
            /// </summary>
            public const string SubscriptionCreated = "subscription.created";
            /// <summary>
            /// ASSINATURA - RENOVADA
            /// </summary>
            public const string SubscriptionRenewed = "subscription.renewed";
            /// <summary>
            /// ASSINATURA - EXPIROU
            /// </summary>
            public const string SubscriptionExpired = "subscription.expired";
            /// <summary>
            /// ASSINATURA - ALTERADA
            /// </summary>
            public const string SubscriptionChanged = "subscription.changed";
            /// <summary>
            /// VERIFICAÇÃO
            /// </summary>
            public const string ReferralsVerification = "referrals.verification";
            /// <summary>
            /// VERIFICAÇÃO DE DADOS BANCARIOS
            /// </summary>
            public const string ReferralsBankVerification = "referrals.bank_verification";
            /// <summary>
            /// PEDIDO DE SAQUE
            /// </summary>
            public const string WithdrawRequestCreated = "withdraw_request.created";
            /// <summary>
            /// PEDIDO DE SAQUE STATUS ALTERADO
            /// </summary>
            public const string WithdrawRequestStatusChanged = "withdraw_request.status_changed";

        }
    }
}
