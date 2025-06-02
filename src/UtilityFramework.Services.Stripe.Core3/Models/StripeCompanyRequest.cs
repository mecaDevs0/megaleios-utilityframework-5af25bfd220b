using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Stripe.Core3.Models
{
    /// <summary>
    /// /// Representa uma solicitação de empresa para integração com o Stripe.
    /// </summary>
    public class StripeCompanyRequest : WithDocumentRequest
    {
        /// <summary>
        /// Nome legal da empresa. (required)
        /// </summary>
        [Required]
        public string LegalName { get; set; }

        /// <summary>
        /// Número do CNPJ da empresa. (required)
        /// </summary>
        [Required]
        public string Cnpj { get; set; }

        /// <summary>
        /// Telefone de contato da empresa. (Validação KYC)
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Endereço da empresa.
        /// </summary>
        [Required]
        public StripeAddress Address { get; set; }

        /// <summary>
        /// Representante individual da empresa.
        /// </summary>
        public StripeIndividualRequest Representative { get; set; }
    }
}
