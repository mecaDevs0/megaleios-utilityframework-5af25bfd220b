using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Entity;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class IuguCustomerModel : IuguBaseErrors
    {
        /// <summary>
        /// Identificador
        /// </summary>
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public string Id { get; set; }
        /// <summary>
        /// E-mail
        /// </summary>
        [JsonProperty("email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }
        /// <summary>
        /// Nome
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        /// <summary>
        /// Notas
        /// </summary>
        [JsonProperty("notes", NullValueHandling = NullValueHandling.Ignore)]
        public string Notes { get; set; }
        /// <summary>
        /// Data de cadastro
        /// </summary>
        [JsonProperty("created_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? CreatedAt { get; set; }
        /// <summary>
        /// Ultima atualização
        /// </summary>
        [JsonProperty("updated_at", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Endereços de E-mail para cópia separados por vírgula
        /// </summary>
        [JsonProperty("cc_emails", NullValueHandling = NullValueHandling.Ignore)]
        public string CcEmails { get; set; }
        /// <summary>
        /// CPF OU CNPJ
        /// </summary>
        [JsonProperty("cpf_cnpj", NullValueHandling = NullValueHandling.Ignore)]
        public string CpfCnpj { get; set; }
        /// <summary>
        /// CEP
        /// </summary>
        [JsonProperty("zip_code", NullValueHandling = NullValueHandling.Ignore)]
        public string ZipCode { get; set; }
        /// <summary>
        /// Rua
        /// </summary>
        [JsonProperty("street", NullValueHandling = NullValueHandling.Ignore)]
        public string Street { get; set; }
        /// <summary>
        /// Número do endereço (obrigatório caso zip_code seja enviado)
        /// </summary>
        [JsonProperty("number", NullValueHandling = NullValueHandling.Ignore)]
        public string Number { get; set; }
        /// <summary>
        /// Complemento de endereço. Ponto de referência.
        /// </summary>
        [JsonProperty("complement", NullValueHandling = NullValueHandling.Ignore)]
        public string Complement { get; set; }
        /// <summary>
        /// Cidade
        /// </summary>
        [JsonProperty("city", NullValueHandling = NullValueHandling.Ignore)]
        public string City { get; set; }
        /// <summary>
        /// Estado
        /// </summary>
        [JsonProperty("state", NullValueHandling = NullValueHandling.Ignore)]
        public string State { get; set; }
        /// <summary>
        /// Bairro
        /// </summary>
        [JsonProperty("district", NullValueHandling = NullValueHandling.Ignore)]
        public string District { get; set; }
        /// <summary>
        /// Prefixo, apenas números - 3 dígitos (obrigatório caso preencha "phone")
        /// </summary>
        [JsonProperty("phone_prefix", NullValueHandling = NullValueHandling.Ignore)]
        public int? PhonePrefix { get; set; }
        /// <summary>
        /// Número do Telefone - 9 dígitos
        /// </summary>
        [JsonProperty("phone", NullValueHandling = NullValueHandling.Ignore)]
        public int? Phone { get; set; }
        /// <summary>
        /// ID da Forma de Pagamento padrão do cliente, "id" obtido em "Listar formas de pagamentos", quando preenchido, vai fazer cobranças automáticas das faturas geradas para este cliente, na data do vencimento ou após vencimento. Enviar "null" para desvincular o cartão das cobranças automáticas.
        /// </summary>
        [JsonProperty("default_payment_method_id", NullValueHandling = NullValueHandling.Ignore)]
        public string DefaultPaymentMethodId { get; set; }
        /// <summary>
        /// Variáveis Personalizadas, adicionar, alterar ou remover. Para remover, envie "name:$valor" + "_destroy:true" - $valor é a própria chave para remoção do item.
        /// </summary>
        [JsonProperty("custom_variables", NullValueHandling = NullValueHandling.Ignore)]
        public List<CustomVariablesModel> CustomVariables { get; set; } = new List<CustomVariablesModel>();

    }
}