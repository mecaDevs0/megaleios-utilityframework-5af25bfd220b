
using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeExternalAccountMarketPlaceRequest
    {
        /// <summary>
        /// País da conta bancária. Valor padrão: "BR".
        /// </summary>
        public string Country { get; set; } = "BR";

        /// <summary>
        /// Moeda da conta bancária. Valor padrão: "BRL".
        /// </summary>
        public string Currency { get; set; } = "BRL";

        /// <summary>
        /// Código do banco. EX: ###
        /// </summary>
        [Required]
        public string BankCode { get; set; }

        /// <summary>
        /// Número da agência bancária. EX: ####-#
        /// </summary>
        [Required]
        public string AgencyNumber { get; set; }

        /// <summary>
        /// Número da conta bancária. EX: #######-#
        /// </summary>
        [Required]
        public string AccountNumber { get; set; }

        /// <summary>
        /// Tipo de titular da conta bancária (pessoa física ou jurídica). Valor padrão: "pessoa física".
        /// </summary>
        public EStripeHolderType HolderType { get; set; } = EStripeHolderType.Individual;

        /// <summary>
        /// Nome do titular da conta bancária.
        /// </summary>
        [Required]
        public string HolderName { get; set; }
    }
}
