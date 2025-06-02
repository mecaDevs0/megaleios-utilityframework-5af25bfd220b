namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class WithDocumentRequest
    {
        /// <summary>
        /// Documento de identificação.
        /// </summary>
        /// <value></value>
        public string DocumentFront { get; set; }
        /// <summary>
        /// Documento de identificação.
        /// </summary>
        /// <value></value>
        public string DocumentBack { get; set; }
        /// <summary>
        /// Obrigatório envio dos documentos
        /// Caso não seja informado o envio dos documentos, o Stripe irá solicitar o envio dos documentos gerando pendência a ser tratada
        /// </summary>
        /// <value></value>
        public bool RequiredDocument { get; set; } = true;
    }
}
