using System.Collections.Generic;
using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    /// <summary>
    /// Model que representa dados do cliente
    /// </summary>
    public class CustomerModel
    {
        /// <summary>
        /// ID do clinte
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }
        /// <summary>
        /// Email do cliente
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }
        /// <summary>
        /// Nome do cliente (opcional)
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }
        /// <summary>
        /// Anotações Gerais do Cliente (opcional)
        /// </summary>
        [JsonProperty("notes")]
        public string Notes { get; set; }
        /// <summary>
        /// Data de criação do Cliente (opcional)
        /// </summary>
        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
        /// <summary>
        /// Data de modificação do Cliente (opcional)
        /// </summary>
        [JsonProperty("updated_at")]
        public string UpdatedAt { get; set; }
        /// <summary>
        /// Variáveis Personalizadas do Cliente (opcional)
        /// </summary>
        [JsonProperty("custom_variables")]
        public List<CustomVariablesModel> CustomVariables { get; set; } = new List<CustomVariablesModel>();

        [JsonProperty("cc_emails")]
        public string CcEmails { get; set; }

        [JsonProperty("complement")]
        public string Complement { get; set; }

        [JsonProperty("cpf_cnpj")]
        public string CpfCnpj { get; set; }

        [JsonProperty("datasource_id")]
        public string DatasourceId { get; set; }

        [JsonProperty("default_payment_method_id")]
        public string DefaultPaymentMethodId { get; set; }

        [JsonProperty("district")]
        public string District { get; set; }

        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }

        [JsonProperty("phone_prefix")]
        public string PhonePrefix { get; set; }

        [JsonProperty("proxy_payments_from_customer_id")]
        public string ProxyPaymentsFromCustomerId { get; set; }

        [JsonProperty("street")]
        public string Street { get; set; }

        [JsonProperty("trade_name")]
        public string TradeName { get; set; }

        [JsonProperty("zip_code")]
        public string ZipCode { get; set; }
    }
}
