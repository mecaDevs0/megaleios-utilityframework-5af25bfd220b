using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{

    public class TransactionViewModel : BaseTransactionViewModel
    {
        public TransactionViewModel()
        {
            Currency = CurrencyType.BRL;
            InstallmentPlan = new InstallmentPlanViewModel();
        }
        /// <summary>
        /// CONFIGURAÇÕES DE PAGAMENTO
        /// </summary>
        /// <value></value>

        [JsonProperty("source")]
        public SourceViewModel Source { get; set; }
        /// <summary>
        /// CONFIGURAÇÕES DE PARCELAMENTO
        /// </summary>
        /// <value></value>

        [JsonProperty("installment_plan")]
        public InstallmentPlanViewModel InstallmentPlan { get; set; }

        /// <summary>
        /// descritor de instrução
        /// </summary>
        /// <value></value>
        [JsonProperty("statement_descriptor")]
        public string StatementDescriptor { get; set; }

        /// <summary>
        /// ID DO COMPRADOR
        /// </summary>
        /// <value></value>
        [JsonProperty("customer")]
        public string Customer { get; set; }
        /// <summary>
        /// TOKEN DO CARTÃO
        /// </summary>
        /// <value></value>

        [JsonProperty("token")]
        public string Token { get; set; }
        /// <summary>
        /// DADOS DO COMPRADOR
        /// </summary>
        /// <value></value>
        [JsonProperty("buyer")]

        public BuyerViewModel Buyer { get; set; }
    }

}