using System.ComponentModel;
using System.Runtime.Serialization;

namespace UtilityFramework.Services.Stripe.Core3
{
    /// <summary>
    /// Tipos de conta do Stripe.
    /// </summary>
    public enum EStripeAccountType
    {
        /// <summary>
        /// Conta padrão.
        /// </summary>
        [EnumMember(Value = "standard")]
        [Description("standard")]
        Standard = 0,

        /// <summary>
        /// Conta expressa.
        /// </summary>
        [EnumMember(Value = "express")]
        [Description("express")]
        Express = 1,

        /// <summary>
        /// Conta personalizada.
        /// </summary>
        [EnumMember(Value = "custom")]
        [Description("custom")]
        Custom = 2
    }

    public enum EStripeGender
    {
        /// <summary>
        /// Masculino
        /// </summary>
        [EnumMember(Value = "male")]
        [Description("male")]
        Male = 0,
        /// <summary>
        /// Feminino
        /// </summary>
        [EnumMember(Value = "female")]
        [Description("female")]
        Female = 1,
    }

    public enum EStripeFilePurpose
    {
        [EnumMember(Value = "account_requirement")]
        [Description("account_requirement")]
        AccountRequirement,

        [EnumMember(Value = "additional_verification")]
        [Description("additional_verification")]
        AdditionalVerification,

        [EnumMember(Value = "business_icon")]
        [Description("business_icon")]
        BusinessIcon,

        [EnumMember(Value = "business_logo")]
        [Description("business_logo")]
        BusinessLogo,

        [EnumMember(Value = "customer_signature")]
        [Description("customer_signature")]
        CustomerSignature,

        [EnumMember(Value = "dispute_evidence")]
        [Description("dispute_evidence")]
        DisputeEvidence,

        [EnumMember(Value = "identity_document")]
        [Description("identity_document")]
        IdentityDocument,

        [EnumMember(Value = "issuing_regulatory_reporting")]
        [Description("issuing_regulatory_reporting")]
        IssuingRegulatoryReporting,

        [EnumMember(Value = "pci_document")]
        [Description("pci_document")]
        PciDocument,

        [EnumMember(Value = "tax_document_user_upload")]
        [Description("tax_document_user_upload")]
        TaxDocumentUserUpload,

        [EnumMember(Value = "terminal_reader_splashscreen")]
        [Description("terminal_reader_splashscreen")]
        TerminalReaderSplashscreen
    }

    public enum EStripePayoutSchedule
    {
        /// <summary>
        /// Manual
        /// </summary>
        [EnumMember(Value = "manual")]
        [Description("manual")]
        Manual = 0,
        /// <summary>
        /// Diário
        /// </summary>
        [EnumMember(Value = "daily")]
        [Description("daily")]
        Daily = 1,
        /// <summary>
        /// Semanal
        /// </summary>
        [EnumMember(Value = "weekly")]
        [Description("weekly")]
        Weekly = 2,
        /// <summary>
        /// Mensal
        /// </summary>
        [EnumMember(Value = "monthly")]
        [Description("monthly")]
        Monthly = 3
    }

    public enum EStripeTypeValue
    {
        /// <summary>
        /// Porcentagem do valor da transação.
        /// </summary>
        [EnumMember(Value = "Porcentagem")]
        [Description("Porccentagem")]
        Percentage = 0,
        /// <summary>
        /// Valor fixo em reais. Ex: R$ 10,00
        /// </summary>
        [EnumMember(Value = "Valor fixo")]
        [Description("Valor fixo")]
        Fixed = 1
    }

    public enum EStripeBusinessType
    {
        /// <summary>
        /// Negócio individual (Pessoa física).
        /// </summary>
        [EnumMember(Value = "individual")]
        [Description("individual")]
        Individual = 0,

        /// <summary>
        /// Empresa (Pessoa Juridica)
        /// </summary>
        [EnumMember(Value = "company")]
        [Description("company")]
        Company = 1,

        /// <summary>
        /// Organização sem fins lucrativos.
        /// </summary>
        [EnumMember(Value = "non_profit")]
        [Description("non_profit")]
        NonProfit = 2,

        /// <summary>
        /// Entidade governamental.
        /// </summary>
        [EnumMember(Value = "government_entity")]
        [Description("government_entity")]
        GovernmentEntity = 3
    }

    public enum EStripeHolderType
    {
        /// <summary>
        /// Negócio individual (Pessoa física).
        /// </summary>
        [EnumMember(Value = "individual")]
        [Description("individual")]
        Individual = 0,

        /// <summary>
        /// Empresa (Pessoa Juridica)
        /// </summary>
        [EnumMember(Value = "company")]
        [Description("company")]
        Company = 1,
    }

    /// <summary>
    /// Tipos de métodos de pagamento suportados pelo Stripe.
    /// </summary>
    public enum EStripePaymentMethodType
    {
        /// <summary>
        /// Cartão de crédito ou débito.
        /// </summary>
        [EnumMember(Value = "card")]
        [Description("card")]
        Card = 0,

        /// <summary>
        /// Boleto bancário.
        /// </summary>
        [EnumMember(Value = "boleto")]
        [Description("boleto")]
        Boleto = 2,

        /// <summary>
        /// Pagamento via Pix.
        /// </summary>
        [EnumMember(Value = "pix")]
        [Description("pix")]
        Pix = 3,
    }

    /// <summary>
    /// Status possíveis para uma intenção de pagamento no Stripe.
    /// </summary>
    public enum EStripePaymentIntentStatus
    {
        /// <summary>
        /// Requer um método de pagamento.
        /// </summary>
        [EnumMember(Value = "requires_payment_method")]
        [Description("requires_payment_method")]
        RequiresPaymentMethod = 0,

        /// <summary>
        /// Requer confirmação.
        /// </summary>
        [EnumMember(Value = "requires_confirmation")]
        [Description("requires_confirmation")]
        RequiresConfirmation = 1,

        /// <summary>
        /// Requer ação adicional do cliente.
        /// </summary>
        [EnumMember(Value = "requires_action")]
        [Description("requires_action")]
        RequiresAction = 2,

        /// <summary>
        /// Pagamento em processamento.
        /// </summary>
        [EnumMember(Value = "processing")]
        [Description("processing")]
        Processing = 3,

        /// <summary>
        /// Pagamento concluído com sucesso.
        /// </summary>
        [EnumMember(Value = "succeeded")]
        [Description("succeeded")]
        Succeeded = 4,

        /// <summary>
        /// Pagamento cancelado.
        /// </summary>
        [EnumMember(Value = "canceled")]
        [Description("canceled")]
        Canceled = 5
    }

    /// <summary>
    /// Status possíveis para uma transferência no Stripe.
    /// </summary>
    public enum EStripeTransferStatus
    {
        /// <summary>
        /// Transferência pendente.
        /// </summary>
        [EnumMember(Value = "pending")]
        [Description("pending")]
        Pending = 0,

        /// <summary>
        /// Transferência paga.
        /// </summary>
        [EnumMember(Value = "paid")]
        [Description("paid")]
        Paid = 1,

        /// <summary>
        /// Transferência falhou.
        /// </summary>
        [EnumMember(Value = "failed")]
        [Description("failed")]
        Failed = 2
    }

    /// <summary>
    /// Status possíveis para um reembolso no Stripe.
    /// </summary>
    public enum EStripeRefundStatus
    {
        /// <summary>
        /// Reembolso pendente.
        /// </summary>
        [EnumMember(Value = "pending")]
        [Description("pending")]
        Pending = 0,

        /// <summary>
        /// Reembolso concluído com sucesso.
        /// </summary>
        [EnumMember(Value = "succeeded")]
        [Description("succeeded")]
        Succeeded = 1,

        /// <summary>
        /// Reembolso falhou.
        /// </summary>
        [EnumMember(Value = "failed")]
        [Description("failed")]
        Failed = 2
    }

    public enum EStripeWeeklyAnchor
    {
        /// <summary>
        /// Monday
        /// </summary>
        [EnumMember(Value = "monday")]
        [Description("monday")]
        Monday = 0,

        /// <summary>
        /// Tuesday
        /// </summary>
        [EnumMember(Value = "tuesday")]
        [Description("tuesday")]
        Tuesday = 1,

        /// <summary>
        /// Wednesday
        /// </summary>
        [EnumMember(Value = "wednesday")]
        [Description("wednesday")]
        Wednesday = 2,

        /// <summary>
        /// Thursday
        /// </summary>
        [EnumMember(Value = "thursday")]
        [Description("thursday")]
        Thursday = 3,

        /// <summary>
        /// Friday
        /// </summary>
        [EnumMember(Value = "friday")]
        [Description("friday")]
        Friday = 4,

        /// <summary>
        /// Saturday
        /// </summary>
        [EnumMember(Value = "saturday")]
        [Description("saturday")]
        Saturday = 5,

        /// <summary>
        /// Sunday
        /// </summary>
        [EnumMember(Value = "sunday")]
        [Description("sunday")]
        Sunday = 6
    }

    public enum EStripeDashboardType
    {
        /// <summary>
        /// Desabilitado
        /// </summary>
        [EnumMember(Value = "none")]
        [Description("none")]
        None = 0,
        /// <summary>
        /// Acesso completo.
        /// </summary>
        [EnumMember(Value = "full")]
        [Description("full")]
        Full = 1,
        /// <summary>
        /// Conta expressa.
        /// </summary>
        [EnumMember(Value = "express")]
        [Description("express")]
        Express = 2,
    }

    public enum StripeAccountRequirement
    {
        [EnumMember(Value = "Conta Bancária")]
        [Description("Conta Bancária")]
        BankAccount,

        [EnumMember(Value = "E-mail")]
        [Description("E-mail")]
        Email,

        [EnumMember(Value = "Telefone")]
        [Description("Telefone")]
        Phone,

        [EnumMember(Value = "Data de Nascimento")]
        [Description("Data de Nascimento")]
        BirthDate,

        [EnumMember(Value = "CPF")]
        [Description("CPF")]
        TaxId,

        [EnumMember(Value = "CNPJ")]
        [Description("CNPJ")]
        BusinessTaxId,

        [EnumMember(Value = "RG/CPF")]
        [Description("RG/CPF")]
        PersonalIdNumber,

        [EnumMember(Value = "Últimos 4 dígitos do SSN")]
        [Description("Últimos 4 dígitos do SSN")]
        SsnLast4,

        [EnumMember(Value = "Documento Adicional")]
        [Description("Documento Adicional")]
        AdditionalVerificationDocument,

        [EnumMember(Value = "Documento de Registro Empresarial")]
        [Description("Documento de Registro Empresarial")]
        BusinessRegistrationDocument,

        [EnumMember(Value = "Documento de Verificação Empresarial")]
        [Description("Documento de Verificação Empresarial")]
        BusinessVerificationDocument,

        [EnumMember(Value = "Documento com Foto")]
        [Description("Documento com Foto")]
        IdentityDocument,

        [EnumMember(Value = "Endereço completo")]
        [Description("Endereço completo")]
        Address,

        [EnumMember(Value = "Descrição produto ou serviço prestado")]
        [Description("Descrição produto ou serviço prestado")]
        BusinessProductDescription,

        [EnumMember(Value = "Aceitar termos")]
        [Description("Aceitar termos")]
        AcceptTerms,

        [EnumMember(Value = "Telefone de suporte")]
        [Description("Telefone de suporte")]
        BusinessSupportPhone,

        [EnumMember(Value = "Url do site")]
        [Description("Url do site")]
        BusinessUrl,

        [EnumMember(Value = "Código MCC")]
        [Description("Código MCC")]
        MerchantCategoryCode,

        [EnumMember(Value = "Razão Social")]
        [Description("Razão Social")]
        CompanyName,

        [EnumMember(Value = "Endereço da Empresa")]
        [Description("Endereço da Empresa")]
        BusinessAddress,

        [EnumMember(Value = "Telefone da Empresa")]
        [Description("Telefone da Empresa")]
        CompanyPhone,

        [EnumMember(Value = "Diretores Informados")]
        [Description("Diretores Informados")]
        DirectorsProvided,

        [EnumMember(Value = "Sócios Informados")]
        [Description("Sócios Informados")]
        OwnersProvided,

        [EnumMember(Value = "Executivos Informados")]
        [Description("Executivos Informados")]
        ExecutivesProvided,

        [EnumMember(Value = "Nome do Representante")]
        [Description("Nome do Representante")]
        LegalRepresentativeName,

        [EnumMember(Value = "Data Nasc. Representante")]
        [Description("Data Nasc. Representante")]
        LegalRepresentativeBirthDate,

        [EnumMember(Value = "Endereço Representante")]
        [Description("Endereço Representante")]
        LegalRepresentativeAddress,

        [EnumMember(Value = "Email Representante")]
        [Description("Email Representante")]
        LegalRepresentativeEmail,

        [EnumMember(Value = "Telefone Representante")]
        [Description("Telefone Representante")]
        LegalRepresentativePhone,

        [EnumMember(Value = "Exposição Política")]
        [Description("Exposição Política")]
        PoliticalExposure,

        [EnumMember(Value = "Cargo")]
        [Description("Cargo")]
        JobTitle,

        [EnumMember(Value = "Confirmação Executiva")]
        [Description("Confirmação Executiva")]
        ExecutiveConfirmation,

        [EnumMember(Value = "Nome Diretor")]
        [Description("Nome Diretor")]
        DirectorName,

        [EnumMember(Value = "Data Nasc. Diretor")]
        [Description("Data Nasc. Diretor")]
        DirectorBirthDate,

        [EnumMember(Value = "Endereço Diretor")]
        [Description("Endereço Diretor")]
        DirectorAddress,

        [EnumMember(Value = "Email Diretor")]
        [Description("Email Diretor")]
        DirectorEmail,

        [EnumMember(Value = "Nome Sócio")]
        [Description("Nome Sócio")]
        OwnerName,

        [EnumMember(Value = "Data Nasc. Sócio")]
        [Description("Data Nasc. Sócio")]
        OwnerBirthDate,

        [EnumMember(Value = "Endereço Sócio")]
        [Description("Endereço Sócio")]
        OwnerAddress,

        [EnumMember(Value = "Percentual Posse")]
        [Description("Percentual Posse")]
        OwnershipPercentage,

        [EnumMember(Value = "Nome Executivo")]
        [Description("Nome Executivo")]
        ExecutiveName,

        [EnumMember(Value = "Data Nasc. Executivo")]
        [Description("Data Nasc. Executivo")]
        ExecutiveBirthDate,

        [EnumMember(Value = "Endereço Executivo")]
        [Description("Endereço Executivo")]
        ExecutiveAddress,

        [EnumMember(Value = "Email Executivo")]
        [Description("Email Executivo")]
        ExecutiveEmail,

        [EnumMember(Value = "Nome do Titular")]
        [Description("Nome do Titular")]
        IndividualName,

        [EnumMember(Value = "Pendência não mapeada")]
        [Description("Pendência não mapeada")]
        Unknown = 999
    };


    public enum EStripePaymentStatus
    {
        [EnumMember(Value = "Pendente")]
        [Description("Pendente")]
        Pending,
        [EnumMember(Value = "Pre-Autorizado")]
        [Description("Pre-Autorizado")]
        PreAuthorized,
        [EnumMember(Value = "Pago")]
        [Description("Pago")]
        Paid,
        [EnumMember(Value = "Estornado")]
        [Description("Estornado")]
        Refunded,
        [EnumMember(Value = "Reembolsado parcialmente")]
        [Description("Reembolsado parcialmente")]
        RefundedPartailly,
        [EnumMember(Value = "Disputado")]
        [Description("Disputado")]
        Disputed,
        [EnumMember(Value = "Cancelado")]
        [Description("Cancelado")]
        Cancelled,
        [EnumMember(Value = "Recusado")]
        [Description("Recusado")]
        Rejected
    }

}