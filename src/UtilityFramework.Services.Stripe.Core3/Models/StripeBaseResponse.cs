namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public partial class StripeBaseResponse<T> : StripeBaseResponse
    {
        /// <summary>
        /// Objeto de retorno
        /// </summary>
        /// <value></value>
        public T Data { get; set; }
    }

    public class StripeBaseResponse
    {
        /// <summary>
        /// Indicador de sucesso da operação
        /// </summary>
        /// <value></value>
        public bool Success { get; set; }
        /// <summary>
        /// Mensagem de erro STRIPE ou erro customizado
        /// </summary>
        /// <value></value>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Stack trace do erro
        /// </summary>
        /// <value></value>
        public string StackTrace { get; set; }
        /// <summary>
        /// Código do erro
        /// </summary>
        /// <value></value>
        public string ErrorCode { get; set; }
        /// <summary>
        /// Tipo de erro
        /// </summary>
        /// <value></value>
        public string ErrorType { get; set; }
        /// <summary>
        /// Parametro que gerou o erro
        /// </summary>
        /// <value></value>
        public string ErrorParam { get; set; }
    }
}
