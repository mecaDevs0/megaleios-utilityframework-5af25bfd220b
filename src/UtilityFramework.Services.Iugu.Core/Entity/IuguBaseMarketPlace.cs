using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    /// <summary>
    /// Modelo que representa os dados do cliente que efetua o pagamento
    /// </summary>
    public class IuguBaseMarketPlace : IuguBaseErrors
    {
        // DATA BANK INFO
        public string AccoutableName { get; set; }
        public string AccoutableCpf { get; set; }
        public string BankAccount { get; set; }
        public string BankAgency { get; set; }
        /// <summary>
        /// CÓDIGO DO BANCO
        /// </summary>
        public string Bank { get; set; }
        /// <summary>
        /// NOME DO BANCO
        /// </summary>
        public string BankName { get; set; }
        public string AccountType { get; set; }
        public string PersonType { get; set; }
        public string Cpnj { get; set; }

        /*KEYS IUGU*/

        public string AccountKey { get; set; }
        public string LiveKey { get; set; }
        public string TestKey { get; set; }
        public string UserApiKey { get; set; }


        /* INFORMA SOBRE VALIDAÇÃO DA CONTA*/
        public bool HasDataBank { get; set; }
        public long? LastRequestVerification { get; set; }
        public long? LastConfirmDataBank { get; set; }
        public bool IsNewRegister { get; set; }
        /// <summary>
        /// INFORMA SE FOI UMA ATUALIZAÇÃO DE DADOS BANCARIOS 
        /// </summary>
        /// <value></value>
        public long? UpdateDataBank { get; set; }
        /// <summary>
        /// INFORMA SE ESTA EM VERIFICAÇÃO
        /// </summary>
        /// <value></value>
        public long? InVerification { get; set; }
        public string Cnpj { get; set; }
        public string CustomMessage { get; set; }
    }
}
