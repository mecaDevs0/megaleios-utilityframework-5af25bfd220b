using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguAccountFilesModel
    {
        /// <summary>
        /// Documento de Identificação Pessoal do Titular (RG, CNH ou Passaporte)
        /// </summary>
        [JsonProperty("identification")]
        public string Identification { get; set; }

        /// <summary>
        /// Foto (selfie) do titular da Conta
        /// </summary>
        [JsonProperty("selfie")]
        public string Selfie { get; set; }

        /// <summary>
        /// Comprovante de Endereço do Titular da Conta (contas de luz, energia, água ou internet) dos últimos 90 dias. Se person_type é Pessoa Jurídica, envie Comprovante de Endereço da companhia.
        /// </summary>
        [JsonProperty("address_proof")]
        public string AddressProof { get; set; }

        /// <summary>
        /// Relatório financeiro da Companhia. Obrigatório se person_type é Pessoa Jurídica
        /// </summary>
        [JsonProperty("balance_sheet")]
        public string BalanceSheet { get; set; }

        /// <summary>
        /// Contrato Social. Obrigatório se person_type é Pessoa Jurídica
        /// </summary>
        [JsonProperty("social_contract")]
        public string SocialContract { get; set; }

        /// <summary>
        /// Documento adicional 1
        /// </summary>
        [JsonProperty("additional_document_one")]
        public string AdditionalDocumentOne { get; set; }

        /// <summary>
        /// Documento adicional 2
        /// </summary>
        [JsonProperty("additional_document_two")]
        public string AdditionalDocumentTwo { get; set; }
    }
}