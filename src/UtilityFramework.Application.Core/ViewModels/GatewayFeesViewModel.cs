using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Application.Core.ViewModels
{
    public class GatewayFeesViewModel
    {
        /// <summary>
        /// Taxa da mega (%)
        /// </summary>
        [Display(Name = "Taxa da mega (%)")]
        public double Mega { get; set; }
        /// <summary>
        /// Taxas da iugu
        /// </summary>
        [Display(Name = "Taxas da iugu")]
        public IuguFeesViewModel Iugu { get; set; }
    }
}