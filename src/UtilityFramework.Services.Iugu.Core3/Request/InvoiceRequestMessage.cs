using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using UtilityFramework.Application.Core3;
using UtilityFramework.Services.Iugu.Core3.Entity;
using UtilityFramework.Services.Iugu.Core3.Models;


namespace UtilityFramework.Services.Iugu.Core3.Request
{
    /// <summary>
    /// Objeto que representa request de criação de fartura
    /// </summary>
    public class InvoiceRequestMessage : IuguBaseErrors
    {
        public InvoiceRequestMessage()
        {
            Items = new List<Item>();
            Logs = new List<Logs>();
            EarlyPaymentDiscounts = new List<EarlyPaymentDiscountsModel>();
            CustomVariables = new List<CustomVariablesModel>();
            PayableWith = "bank_slip";
            EnsureWorkdayDueDate = true;
            Payer = new PayerModel();

        }

        /// <summary>
        /// E-Mail do cliente
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// Data de Expiração (DD/MM/AAAA) - (AAAA/MM/DD)
        /// </summary>
        [JsonProperty("due_date")]
        [Display(Name = "Data de expiração")]
        public string DueDate { get; set; }

        /// <summary>
        ///  Itens da Fatura
        /// </summary>
        [JsonProperty("items")]
        public List<Item> Items { get; set; }

        /// <summary>
        /// Informações do Cliente para o Anti Fraude ou Boleto
        /// </summary>
        [JsonProperty("payer")]
        [IsClass]
        public PayerModel Payer { get; set; }

        /// <summary>
        /// (opcional) Cliente é redirecionado para essa URL após efetuar o pagamento da Fatura pela página de Fatura da Iugu
        /// </summary>
        [JsonProperty("return_url ")]
        public string ReturnUrl { get; set; }

        /// <summary>
        /// (opcional) Cliente é redirecionado para essa URL se a Fatura que estiver acessando estiver expirada
        /// </summary>
        [JsonProperty("expired_url ")]
        public string ExpiredUrl { get; set; }

        /// <summary>
        /// (opcional) URL chamada para todas as notificações de Fatura, assim como os webhooks (Gatilhos) são chamados
        /// </summary>
        [JsonProperty("notification_url")]
        public string NotificationUrl { get; set; }

        /// <summary>
        /// (opcional) Valor dos Impostos em centavos
        /// </summary>
        [JsonProperty("tax_cents")]
        public int TaxCents { get; set; }

        /// <summary>
        /// (opcional) Booleano para Habilitar ou Desabilitar multa por atraso de pagamento
        /// </summary>
        [JsonProperty("fines")]
        [Display(Name = "Multa de atraso")]

        public bool Fines { get; set; }

        /// <summary>
        /// (opcional) Determine a multa a ser cobrada para pagamentos efetuados após a data de vencimento
        /// </summary>
        [JsonProperty("late_payment_fine")]
        [Display(Name = "Multa após vencimento")]

        public string LatePaymentFine { get; set; }

        /// <summary>
        /// (opcional) Booleano que determina se cobra ou não juros por dia de atraso. 1% ao mês pro rata.
        /// </summary>
        [JsonProperty("per_day_interest")]
        [Display(Name = "Juros por dia")]

        public bool PerDayInterest { get; set; }

        /// <summary>
        /// (opcional) Valor dos Descontos em centavos
        /// </summary>
        [JsonProperty("discount_cents")]

        public int DiscountCents { get; set; }

        /// <summary>
        /// (opcional) ID do Cliente
        /// </summary>
        [JsonProperty("customer_id")]
        public string CustomerId { get; set; }

        /// <summary>
        /// (opcional) Booleano que ignora o envio do e-mail de cobrança
        /// </summary>
        [JsonProperty("ignore_due_email")]
        public bool IgnoreDueEmail { get; set; }

        /// <summary>
        /// (opcional) Amarra esta Fatura com a Assinatura especificada
        /// </summary>
        [JsonProperty("subscription_id")]
        public string SubscriptionId { get; set; }

        /// <summary>
        /// (opcional) Método de pagamento que será disponibilizado para esta Fatura (‘all’, ‘credit_card’ ou ‘bank_slip’).
        /// Obs: Caso esta Fatura esteja atrelada à uma Assinatura, a prioridade é herdar o valor atribuído na Assinatura;
        /// caso esta esteja atribuído o valor ‘all’, o sistema considerará o payable_with da Fatura; se não, o sistema considerará o payable_with da Assinatura.
        /// </summary>
        [JsonProperty("payable_with")]
        [Display(Name = "Metodo de pagamento aceito")]

        public string PayableWith { get; set; }

        /// <summary>
        /// (opcional) Caso tenha o subscription_id, pode-se enviar o número de créditos a adicionar nessa Assinatura quando a Fatura for paga
        /// </summary>
        [JsonProperty("credits")]
        public int? Credits { get; set; }

        /// <summary>
        /// (opcional) Logs da Fatura
        /// </summary>
        [JsonProperty("logs")]
        public List<Logs> Logs { get; set; }

        /// <summary>
        /// (opcional) Variáveis Personalizadas
        /// </summary>
        [JsonProperty("custom_variables")]
        public List<CustomVariablesModel> CustomVariables { get; set; }

        /// <summary>
        /// (Opcional) Ativa ou desativa os descontos por pagamento antecipado. Quando true, sobrepõe as configurações de desconto da conta
        /// </summary>
        /// <value></value>
        [JsonProperty("early_payment_discount")]
        public bool EarlyPaymentDiscount { get; set; }

        /// <summary>
        /// (Opcional) Quantidade de dias de antecedência para o pagamento receber o desconto (Se enviado, substituirá a configuração atual da conta)
        /// </summary>
        /// <value></value>
        [JsonProperty("early_payment_discounts")]
        public List<EarlyPaymentDiscountsModel> EarlyPaymentDiscounts { get; set; }

        /// <summary>
        /// (Opcional) Se true, garante que a data de vencimento seja apenas em dias de semana, e não em sábados ou domingos.
        /// </summary>
        /// <value></value>
        [JsonProperty("ensure_workday_due_date")]
        public bool EnsureWorkdayDueDate { get; set; }

        /// <summary>
        /// (Opcional) Endereços de E-mail para cópia separados por ponto e vírgula.
        /// </summary>
        /// <value></value>
        [JsonProperty("cc_emails")]
        public string CcEmails { get; set; }
        /// <summary>
        /// (Opcional) Número único que identifica o pedido de compra. Opcional, ajuda a evitar o pagamento da mesma fatura.
        /// </summary>
        /// <value></value>
        [JsonProperty("order_id")]
        public string OrderId { get; set; }
        /// <summary>
        ///
        /// </summary>
        [JsonProperty("commissions")]
        public CommissionModel Commissions { get; set; }
        /// <summary>
        /// Lista de splits a serém aplicado nas faturas pagas. Para mais informações sobre com funciona o split por fatura https://dev.iugu.com/docs/split-por-fatura-no-cart%C3%A3o-de-cr%C3%A9dito-por-api
        /// </summary>
        [JsonProperty("splits")]
        public List<IuguSplitModel> Splits { get; set; }
        /// <summary>
        /// Expira uma fatura e impossibilita o seu pagamento depois 'x' dias após o vencimento. Valor enviado precisa estar entre 1 e 30.
        /// Muita atenção ao utilizar o parâmetro "expires_in". Uma vez que, se enviado durante a criação da fatura, irá sobrescrever qualquer régua de cobrança que já exista na plataforma.
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
        /// <summary>
        /// Prazo máximo para pagamento do boleto após o vencimento. O prazo máximo para pagamento deve ser entre "1" e "30" dias após a data de vencimento.
        /// </summary>
        [JsonProperty("bank_slip_extra_due")]
        public string BankSlipExtraDue { get; set; }
        /// <summary>
        /// Desliga o e-mail de cancelamento de fatura
        /// </summary>
        [JsonProperty("ignore_canceled_email")]
        public bool IgnoreCanceledEmail { get; set; }
        /// <summary>
        /// Determine a multa por valor fixo a ser cobrada para pagamentos efetuados após a data de vencimento
        /// </summary>
        [JsonProperty("late_payment_fine_cents")]
        public int? LatePaymentFineCents { get; set; }
        /// <summary>
        /// Informar o valor percentual de juros que deseja cobrar
        /// </summary>
        [JsonProperty("per_day_interest_value")]
        public int? PerDayInterestValue { get; set; }
        /// <summary>
        /// Juros a ser cobrado por dia em centavos. Tem prioridade sobre per_day_interest_value. O juros somado em 30 dias deve ser inferior a 50% do valor da fatura.
        /// </summary>
        [JsonProperty("per_day_interest_cents")]
        public int? PerDayInterestCents { get; set; }
        /// <summary>
        /// ID da external da plataforma
        /// </summary>
        [JsonProperty("external_reference")]
        public string ExternalReference { get; set; }
        /// <summary>
        /// Limita um valor máximo de parcelas. Caso não for enviado, pegará o padrão configurado na conta.
        /// </summary>
        [JsonProperty("max_installments_value")]
        public int? MaxInstallmentsValue { get; set; }
    }
}
