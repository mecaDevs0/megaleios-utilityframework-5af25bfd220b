namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class BankInfoViewModel
    {
        /// <summary>
        /// Identificador do Banco na base
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Nome do banco
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Código do banco
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// MASK de preenchimento do campo AGENCIA
        /// </summary>
        public string AgencyMask { get; set; }
        /// <summary>
        /// MASK de preenchimento do campo CONTA
        /// </summary>
        public string AccountMask { get; set; }
    }
}
