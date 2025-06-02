using System.ComponentModel;
using System.Runtime.Serialization;

namespace UtilityFramework.Services.Iugu.Core3.Enums
{

    public enum IuguPaymentTypeEnum
    {
        [EnumMember(Value = "All")]
        [Description("All")]
        all = 0,
        [EnumMember(Value = "Cartão de crédito")]
        [Description("Cartão de crédito")]
        credit_card = 1,
        [EnumMember(Value = "Boleto bancário")]
        [Description("Boleto bancário")]
        bank_split = 2,
        [EnumMember(Value = "Pix")]
        [Description("Pix")]
        pix = 3
    }
    public enum FileDocumentStatus
    {
        [EnumMember(Value = "Análise pendente do nosso Time Prevenção à Fraude.")]
        [Description("Análise pendente do nosso Time Prevenção à Fraude.")]
        pending_manual_analysis = 0,
        [EnumMember(Value = "Reenvio de Documentos solicitado pelo Time de Prevenção à Fraude. Utilize o endpoint Reenviar Documentos para Verificar Conta iugu.")]
        [Description("Reenvio de Documentos solicitado pelo Time de Prevenção à Fraude. Utilize o endpoint Reenviar Documentos para Verificar Conta iugu.")]
        requested = 1,
        [EnumMember(Value = "Documento aprovado pelo Time de Prevenção à Fraude.")]
        [Description("Documento aprovado pelo Time de Prevenção à Fraude.")]
        approved = 2
    }

    public enum IuguIntevalType
    {
        [EnumMember(Value = "Semanas")]
        [Description("Semanas")]
        weeks = 0,
        [EnumMember(Value = "Meses")]
        [Description("Meses")]
        months = 1
    }



    /// <summary>
    /// Tipo de intervalos de plano
    /// </summary>
    public enum PlanIntervalType
    {
        /// <summary>
        /// Plano com um ciclo semanal
        /// </summary>
        Weekly,
        /// <summary>
        /// Plano com um ciclo mensal
        /// </summary>
        Monthly
    }

    /// <summary>
    /// Moedas suportadas
    /// </summary>
    public enum CurrencyType
    {
        /// <summary>
        /// Moeda brasileira
        /// </summary>
        BRL
    }

    /// <summary>
    /// Bancos disponíveis
    /// </summary>
    public enum AvailableBanks
    {
        CaixaEconomicaFederal = 104, BancoDoBrasil = 001, Bradesco = 237, Itau = 341, Santander = 033, HSBC = 399
    }

    /// <summary>
    /// Status da fatura
    /// </summary>
    public enum InvoiceAvailableStatus
    {
        Paid, Canceled, PartiallyPaid, Pending, Draft, Refunded, Expired
    }

    /// <summary>
    /// Person type
    /// </summary>
    public enum PersonType
    {
        /// <summary>
        /// Pessoa Jurídica
        /// </summary>
        CorporateEntity,

        /// <summary>
        /// Pessoa física
        /// </summary>
        IndividualPerson
    }

    /// <summary>
    /// Bank account type
    /// </summary>
    public enum BankAccountType
    {
        /// <summary>
        /// Conta poupança
        /// </summary>
        SavingAccount,

        /// <summary>
        /// Conta Corrente
        /// </summary>
        CheckingAccount
    }

    public enum BankAccountTypeAbbreviation
    {
        /// <summary>
        /// Conta poupança
        /// </summary>
        CP,

        /// <summary>
        /// Conta Corrente
        /// </summary>
        CC
    }

    /// <summary>
    /// Metodos de pagamento suportado
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Todos os tipos de pagamentos serão aceitos
        /// </summary>
        All,
        /// <summary>
        /// Cartão de crédito
        /// </summary>
        CreditCard,

        /// <summary>
        /// Boleto bancário
        /// </summary>
        BankSlip,
        /// <summary>
        /// Pix
        /// </summary>
        Pix
    }

    /// <summary>
    /// Tipo de Ordenaçãp
    /// </summary>
    public enum ResultOrderType
    {
        /// <summary>
        /// Menor para o maior
        /// </summary>
        Ascending,
        /// <summary>
        /// Maior para o menor
        /// </summary>
        Descending
    }

    /// <summary>
    /// Campos com Ordenações suportadas
    /// </summary>
    public enum FieldSort
    {
        Id,
        Status,
        CreateAt,
        UpdateAt,
        Amount,
        AccountName,
        Name,
    }

    public enum Permission
    {
        [EnumMember(Value = "owner")]
        [Description("owner")]
        Administrator = 0,
        [EnumMember(Value = "analytics")]
        [Description("analytics")]
        Analytics = 1,
        [EnumMember(Value = "operations")]
        [Description("operations")]
        Operations = 2,
        [EnumMember(Value = "financial")]
        [Description("financial")]
        Financial = 3,
        [EnumMember(Value = "operations-read-only")]
        [Description("operations-read-only")]
        OperationsReadOnly = 4,
        [EnumMember(Value = "financial-read-only")]
        [Description("financial-read-only")]
        FinancialReadOnly = 5,

    }

}