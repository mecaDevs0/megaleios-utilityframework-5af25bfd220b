namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeBaseAddressRequest
    {
        /// <summary>
        /// Rua
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// Código postal
        /// </summary>
        public string ZipCode { get; set; }
    }
}
