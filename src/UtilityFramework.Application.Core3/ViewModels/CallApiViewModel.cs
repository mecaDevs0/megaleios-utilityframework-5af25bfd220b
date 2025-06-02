using RestSharp;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace UtilityFramework.Application.Core3.ViewModels
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
        /// Arquivos (Caminho para os arquivos)
        /// </summary>
        [Display(Name = "Arquivos")]
        public List<string> PathFiles { get; set; } = new List<string>();
        /// <summary>
        /// VERBO HTTP
        /// </summary>
        [Display(Name = "VERBO HTTP")]
        public Method Method { get; set; } = Method.GET;
        /// <summary>
        /// Certificados
        /// </summary>
        [Display(Name = "Certificados")]
        public X509CertificateCollection Certificates { get; set; }
        public bool AlwaysMultipartFormData { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new Dictionary<string, string>();
    }
}
