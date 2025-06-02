using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class InstallmentPlanViewModel
    {
        public InstallmentPlanViewModel(SourceInstallmentPlanMode mode = SourceInstallmentPlanMode.InterestFree, int number_installments = 1)
        {
            Mode = mode;
            NumberInstallments = number_installments;
        }
        /// <summary>
        /// MODO DE PARCELAMENTO  | use o enum SourceInstallmentPlanMode
        /// </summary>
        /// <value></value>
        [JsonProperty("mode")]
        public SourceInstallmentPlanMode Mode { get; set; }

        /// <summary>
        /// NUMERO DE PARCELAS 
        /// </summary>
        /// <value></value>
        [JsonProperty("number_installments")]
        public int NumberInstallments { get; set; }
    }

}