using Stripe;


namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeMarketPlaceRequest
    {
        /// <summary>
        /// País associado à conta. Deve ser um código de país ISO de duas letras.
        /// </summary>
        public string Country { get; set; } = "BR";
        /// <summary>
        /// Código de moeda ISO de três letras representando a moeda padrão para a conta.
        /// Esta deve ser uma moeda que a Stripe suporta no país da conta.
        /// </summary>
        /// <value>BRL</value>
        public string DefaultCurrency { get; set; } = "BRL";
        /// <summary>
        /// Aceitar termos
        /// </summary>
        /// <value></value>
        public string RemoteIp { get; set; }
        /// <summary>
        /// Informa se aceitou os termos
        /// Informa false para não aceitar termos automaticamente, caso aceite informe REMOTEIP do usuário e USERAGENT
        /// </summary>
        /// <value></value>
        public bool? AcceptTerms { get; set; }
        /// <summary>
        /// Informar caso aceite os termos
        /// </summary>
        /// <value></value>
        public string UserAgent { get; set; }

        /// <summary>
        /// E-mail da subconta
        /// </summary>
        /// <value></value>
        public string Email { get; set; }
        /// <summary>
        /// E-mail de suporte
        /// </summary>
        /// <value></value>
        public string SupportEmail { get; set; }

        /// <summary>
        /// Configurações de capacidades da conta, como transferências, pagamentos com cartão, etc. (opcional)
        /// </summary>
        public AccountCapabilitiesOptions Capabilities { get; set; }

        /// <summary>
        /// Configuração do cronograma de pagamentos da conta Stripe.
        /// </summary>
        public EStripePayoutSchedule PayoutSchedule { get; set; } = EStripePayoutSchedule.Monthly;

        /// <summary>
        /// Dia do mês em que o pagamento é feito.
        /// </summary>
        public long? MonthlyAnchor { get; set; }
        /// <summary>
        /// Dia da semana em que o pagamento é feito.
        /// </summary>
        public EStripeWeeklyAnchor? WeeklyAnchor { get; set; }
        /// <summary>
        /// Delay de pagamento em dias (PayoutSchedule = 'manual').
        /// </summary>
        public long? PayoutDelayDays { get; set; }

        /// <summary>
        /// Tipo de negócio associado à conta Stripe, como Individual ou Company.
        /// </summary>
        public EStripeBusinessType BusinessType { get; set; }

        /// <summary>
        /// Informações da conta bancária associada à conta Stripe.
        /// </summary>
        public StripeExternalAccountMarketPlaceRequest BankAccount { get; set; }

        /// <summary>
        /// Informações do indivíduo associado à conta Stripe.
        /// </summary>
        public StripeIndividualRequest Individual { get; set; }

        /// <summary>
        /// Informações da empresa associada à conta Stripe.
        /// </summary>
        public StripeCompanyRequest Company { get; set; }
        /// <summary>
        /// Configurações de controle de acesso à conta (Opcional).
        /// </summary>
        /// <value></value>
        public AccountControllerOptions Controller { get; set; }
        /// <summary>
        /// Nome da empresa.
        /// </summary>
        /// <value></value>
        public string BusinessName { get; set; }
        /// <summary>
        /// Descrição do produto associado comercializado.
        /// </summary>
        /// <value></value>
        public string ProductDescription { get; set; }
        /// <summary>
        /// Código de mercado de classificação de comércio (MCC) Default: 5734.
        /// </summary>
        /// <value></value>
        public string Mcc { get; set; }
    }
}
