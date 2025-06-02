using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Application.Core.ViewModels
{
    public class IuguFeesViewModel
    {
        /// <summary>
        /// Taxa pra cartão em 1x (%)
        /// </summary>
        [Display(Name = "Taxa pra cartão em 1x (%)")]
        public double CreditCard { get; set; }
        /// <summary>
        /// Taxa pra cartão em 2x a 6x (%)
        /// </summary>
        [Display(Name = "Taxa pra cartão em 2x a 6x (%)")]
        public double CreditCard2x { get; set; }
        /// <summary>
        /// Taxa pra cartão em 7x a 12x (%)
        /// </summary>
        [Display(Name = "Taxa pra cartão em 7x a 12x (%)")]
        public double CreditCard7x { get; set; }
        /// <summary>
        /// Boleto (R$)
        /// </summary>
        [Display(Name = "Boleto")]
        public double BankSlip { get; set; }
        /// <summary>
        /// Pix (%)
        /// </summary>
        [Display(Name = "Pix (%)")]
        public double Pix { get; set; }
        /// <summary>
        /// Taxa por antecipação
        /// </summary>
        [Display(Name = "Taxa por antecipação")]
        public double Advance { get; set; }
    }
}