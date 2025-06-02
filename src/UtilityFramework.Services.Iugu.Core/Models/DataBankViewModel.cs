using System.ComponentModel.DataAnnotations;

namespace UtilityFramework.Services.Iugu.Core.Models
{
    public class DataBankViewModel
    {
        public DataBankViewModel(bool setDefault = true)
        {
            if (setDefault)
            {
                PersonType = "Pessoa Física";
                AccountType = "Corrente";
                AutomaticValidation = true;
                AutomaticTransfer = false;
                PriceRange = "Mais que R$ 1,00";
                BusinessType = "Prestador de serviço";
                Address = "Av Paulista, 2202";
                Cep = "01310-100";
                City = "São Paulo";
                State = "SP";
            }
        }

        /// <summary>
        /// Nome fantasia (PJ)| | NOME (PF)
        /// </summary>
        [Display(Name = "Nome fantasia (PJ)| | NOME (PF)")]

        public string FantasyName { get; set; }
        /// <summary>
        /// RAZÃO SOCIAL
        /// </summary>
        [Display(Name = "Razão social")]
        public string SocialName { get; set; }
        /// <summary>
        /// NUMERO DA CONTA
        /// </summary>
        /// <value></value>
        [Display(Name = "Número da conta")]

        public string BankAccount { get; set; }
        /// <summary>
        /// NUMERO DA AGENCIA
        /// </summary>
        [Display(Name = "Agência")]
        public string BankAgency { get; set; }
        /// <summary>
        /// NOME DO BANCO OU CODIGO
        /// </summary>
        /// <value></value>
        [Display(Name = "Banco")]

        public string Bank { get; set; }
        /// <summary>
        /// Código do banco
        /// </summary>
        [Display(Name = "Código do banco")]
        public string BankCode { get; set; }
        /// <summary>
        ///  DEFAULT corrente | 'Corrente' 'Poupança'
        /// </summary>
        [Display(Name = "Tipo de conta")]

        public string AccountType { get; set; }
        /// <summary>
        /// ENDEREÇO
        /// </summary>
        [Display(Name = "Endereço")]

        public string Address { get; set; }
        /// <summary>
        /// CASO PESSOA JURIDICA ENVIAR CPF DO TITULAR DA CONTA
        /// </summary>
        [Display(Name = "Titular da conta")]
        public string AccountableCpf { get; set; }
        /// <summary>
        /// CASO PESSOA FISICA
        /// </summary>
        public string Cpf { get; set; }
        [Display(Name = "Telefone responsável")]
        public string AccountablePhone { get; set; }
        /// <summary>
        /// DEFAULT | Pessoa Física  | TYPES = 'Pessoa Física' ou 'Pessoa Jurídica'
        /// </summary>
        public string PersonType { get; set; }

        /// <summary>
        /// Descrição do negócio
        /// </summary>
        [Display(Name = "Descrição do negócio")]

        public string BusinessType { get; set; }
        /// <summary>
        /// CASO PESSOA JURIDICA
        /// </summary>
        [Display(Name = "Cnpj")]

        public string Cnpj { get; set; }
        /// <summary>
        ///  DEFAULT = "Mais que R$ 1,00" |  EX: Valor máximo da venda ('Até R$ 100,00', 'Entre R$ 100,00 e R$ 500,00', 'Mais que R$ 500,00')
        /// </summary>
        [Display(Name = "Valor entre")]
        public string PriceRange { get; set; }
        /// <summary>
        ///  VERIFICAÇÃO AUTOMATICA | DEFAULT = TRUE
        /// </summary>
        [Display(Name = "Verificação automatica")]
        public bool AutomaticValidation { get; set; }
        /// <summary>
        /// SOLICITAR SAQUE AUTOMATICO | DEFAULT = TRUE
        /// </summary>
        [Display(Name = "Solicitar saque automatico")]

        public bool AutomaticTransfer { get; set; }
        /// <summary>
        /// VENDE PRODUTOS FISICOS
        /// </summary>
        [Display(Name = "Vende produtos físicos")]

        public bool PhysicalProducts { get; set; }
        /// <summary>
        /// CEP
        /// </summary>
        [Display(Name = "CEP")]

        public string Cep { get; set; }
        /// <summary>
        /// CIDADE
        /// </summary>
        [Display(Name = "Cidade")]
        public string City { get; set; }
        /// <summary>
        /// ESTADO
        /// </summary>
        [Display(Name = "Estado")]
        public string State { get; set; }
        /// <summary>
        /// DATA DA ULTIMA VERIFICAÇÃO  (UNIX)
        /// </summary>
        [Display(Name = "Data da Última verificação")]

        public long? LastRequestVerification { get; set; }
        /// <summary>
        /// RESPONSAVEL PELA CONTA
        /// </summary>
        [Display(Name = "Responsável")]
        public string AccountableName { get; set; }
        /// <summary>
        /// Campo interno
        /// </summary>
        [Display(Name = "Campo interno")]
        public bool Masked { get; internal set; }

        public bool HasDataBank()
        {
            return !string.IsNullOrEmpty(Bank) ||
           !string.IsNullOrEmpty(Bank) ||
           !string.IsNullOrEmpty(BankAgency) ||
           !string.IsNullOrEmpty(BankCode) ||
           !string.IsNullOrEmpty(BankAccount);
        }

        public void SetMasked(bool masked)
        {
            Masked = masked;
        }
    }
}