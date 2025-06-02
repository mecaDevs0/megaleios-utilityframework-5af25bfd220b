using UtilityFramework.Services.Stripe.Core3.Models;

namespace UtilityFramework.Services.Stripe.Core3
{
    public class StripeAddress : StripeBaseAddressRequest
    {
        /// <summary>
        /// Número
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Complemento
        /// </summary>
        public string Complement { get; set; }

        /// <summary>
        /// Bairro
        /// </summary>
        public string Neighborhood { get; set; }

        /// <summary>
        /// País (padrão: BR)
        /// </summary>
        public string Country { get; set; } = "BR";
    }
}