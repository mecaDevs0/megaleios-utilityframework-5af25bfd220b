using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class SourceViewModel
    {
        public SourceViewModel(SourceType sourceType = SourceType.Card, bool capture = true)
        {
            Usage = SourceUsage.SingleUse;
            Type = sourceType;
            Capture = capture;
        }
        /// <summary>
        /// MODO DE USO  | USAR ENUM SourceUsage
        /// </summary>
        /// <value></value>

        [JsonProperty("usage")]
        public SourceUsage Usage { get; set; }
        // /// <summary>
        // /// VALOR EM CENTAVOS
        // /// </summary>
        // /// <value></value>
        // [JsonProperty("amount")]
        // public long? Amount { get; set; }

        /// <summary>
        /// MOEDA 
        /// </summary>
        /// <value></value>
        [JsonProperty("currency")]
        public CurrencyType Currency { get; set; }
        /// <summary>
        /// DESCRIÇÃO
        /// </summary>
        /// <value></value>

        [JsonProperty("description")]
        public string Description { get; set; }
        /// <summary>
        /// TIPO DE REQUISIÇÃO | usar o enum SourceType 
        /// </summary>
        /// <value></value>

        [JsonProperty("type")]
        public SourceType Type { get; set; }

        [JsonProperty("capture")]
        public bool? Capture { get; set; }

        /// <summary>
        /// VENDEDOR 
        /// </summary>
        /// <value></value>
        [JsonProperty("on_behalf_of")]
        public string OnBehalfOf { get; set; }
        /// <summary>
        /// REFERENCIA INTERNA
        /// </summary>
        /// <value></value>

        [JsonProperty("reference_id")]
        public string ReferenceId { get; set; }

        /// <summary>
        /// DADOS DO CARTÃO
        /// </summary>
        /// <value></value>
        [JsonProperty("card")]
        public CardViewModel Card { get; set; }
        /// <summary>
        /// NUMERO DE PARCELAS
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
        /// COBRAR ATRAVES DO USUARIO
        /// </summary>
        /// <value></value>
        [JsonProperty("customer")]
        public BuyerViewModel Customer { get; set; }
        /// <summary>
        /// COBRAR COM TOKEN DE CARTÃO OU CONTA 
        /// </summary>
        /// <value></value>

        [JsonProperty("token")]
        public PayerViewModel Token { get; set; }
    }

}