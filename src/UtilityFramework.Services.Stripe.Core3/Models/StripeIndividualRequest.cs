namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeIndividualRequest : WithDocumentRequest
    {
        /// <summary>
        /// Nome do indivíduo.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Sobrenome do indivíduo.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Endereço de e-mail do indivíduo.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Número de telefone do indivíduo.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// CPF do indivíduo.
        /// </summary>
        public string Cpf { get; set; }

        /// <summary>
        /// Endereço do indivíduo.
        /// </summary>
        public StripeAddress Address { get; set; }
        /// <summary>
        /// Genero do indivíduo.
        /// </summary>
        public EStripeGender? Gender { get; set; }
        /// <summary>
        /// Data de nascimento do indivíduo (dd/MM/yyyy).
        /// </summary>
        /// <value></value>
        public string BirthDate { get; set; }
    }
}
