namespace UtilityFramework.Application.Core.ViewModels
{
    public class ReturnViewModel
    {
        /// <summary>
        /// OBJ
        /// </summary>
        /// <value></value>
        public object Data { get; set; }
        /// <summary>
        /// INDICADOR DE ERRO
        /// </summary>
        /// <value></value>
        public bool Erro { get; set; }
        /// <summary>
        /// DICTIONARY DE ERROS
        /// </summary>
        /// <value></value>
        public object Errors { get; set; }
        /// <summary>
        /// MENSAGEM DE ERRO TRATADA OU PRIMEIRO VALUE DO DICTONARY ERRORS
        /// </summary>
        /// <value></value>
        public string Message { get; set; }
        /// <summary>
        /// MENSAGEM DE EXCEPTION
        /// </summary>
        /// <value></value>
        public string MessageEx { get; set; }
    }
}