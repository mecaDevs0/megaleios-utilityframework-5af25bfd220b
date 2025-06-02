using RestSharp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Application.Core.ViewModels
{
    public class CallApiViewModel
    {
        /// <summary>
        /// URL COMPLETA A SER CHAMADA
        /// </summary>
        [Display(Name = "URL COMPLETA A SER CHAMADA")]
        public string Url { get; set; }
        /// <summary>
        /// DATA (BODY caso POST OU PATCH)
        /// </summary>
        [Display(Name = "DATA")]
        public object Body { get; set; }
        /// <summary>
        /// VERBO HTTP
        /// </summary>
        [Display(Name = "VERBO HTTP")]
        public Method Method { get; set; } = Method.GET;
        /// <summary>
        /// Headers
        /// </summary>
        [Display(Name = "Headers")]
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
        public bool AlwaysMultipartFormData { get; set; }
        /// <summary>
        /// Arquivos (Caminho dos Arquivos)
        /// </summary>
        [Display(Name = "Arquivos")]
        public List<string> PathFiles { get; set; } = [];
    }
}
