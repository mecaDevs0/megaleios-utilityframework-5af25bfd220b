namespace UtilityFramework.Services.Iugu.Core3.Models
{
    public class FeeViewModel
    {
        /// <summary>
        /// Total de taxas
        /// </summary>

        public double TotalFeesValue { get; set; }
        /// <summary>
        /// Valor da megaleios
        /// </summary>

        public double MegaValue { get; set; }
        /// <summary>
        /// Valor do cliente
        /// </summary>

        public double ClientValue { get; set; }
        /// <summary>
        /// Taxa da iugu
        /// </summary>

        public double IuguValue { get; set; }
        /// <summary>
        /// Taxa por antecipação
        /// </summary>

        public double IuguAdvanceValue { get; set; }
        /// <summary>
        /// Total das taxas da iugu (taxas + antecipação)
        /// </summary>

        public double IuguFeesValue { get; set; }
        /// <summary>
        /// Valor liquido descontando (iugu + cliente + mega)
        /// </summary>

        public double NetValue { get; set; }
        /// <summary>
        /// Valor grosso
        /// </summary>

        public double GrossValue { get; set; }
        public bool Error { get; set; }
        public string ErrorMessage { get; set; }
    }
}