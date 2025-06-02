using System;
using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Response
{
    public class BaseErrorViewModel
    {
        [JsonProperty("error")]
        public ErrorViewModel Error { get; set; }

        /// <summary>
        /// INFORMA SE EXISTE ERRO NA REQUESTA
        /// </summary>
        /// <value></value>
        public bool HasError { get; set; }
        public string MessageErro { get; set; }
        public string MessageEx { get; set; }
        public string ResponseContent { get; set; }

        /// <summary>
        /// FAZ O BIND DE UMA EXECEPTION
        /// </summary>
        /// <param name="ex"></param>
        public void ReturnException(Exception ex)
        {

            MessageErro = "Ocorreu um erro, verifique e tente novamente";
            MessageEx = $"{ex.InnerException} {ex.Message}".Trim();
            HasError = true;
        }


        /// <summary>
        /// FAZ O BIND DO ERRO RETORNADO PELO ZOOP
        /// </summary>
        /// <param name="contentRequest"></param>
        public void CheckHasError(string contentRequest)
        {

            HasError = Error != null;
            MessageErro = Error != null ? ZoopUtilities.MessageByCategoryError(Error.CategoryError) ?? Error?.Message : Error?.Message;
            ResponseContent = contentRequest;
        }
    }
}