using System.Collections.Generic;
using UtilityFramework.Services.Stripe.Core3;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeCustomerRequest
    {
        /// <summary>
        /// Identificador do cliente (apenas para update)
        /// </summary>
        public string CustomerId { get; set; }
        /// <summary>
        /// Nome completo do cliente
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Email do cliente (lowercase)
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Telefone do cliente (+55 11 99999-9999)
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Documento do cliente (apenas números)
        /// </summary>
        public string CpfCnpj { get; set; }
        /// <summary>
        /// Endereço do cliente
        /// </summary>
        public StripeAddress Address { get; set; }
        /// <summary>
        /// Endereço de cobrança do cliente
        /// </summary>
        public StripeAddress BillingAddress { get; set; }
        /// <summary>
        /// Id da forma de pagamento (Cartão)
        /// </summary>
        public string PaymentMethodId { get; set; }
        /// <summary>
        /// Linguagem preferida do cliente
        /// </summary>
        public List<string> PreferredLocales { get; set; } = ["pt-BR"];
    }
}