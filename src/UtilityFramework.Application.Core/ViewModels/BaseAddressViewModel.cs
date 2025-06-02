using System.ComponentModel.DataAnnotations;
using UtilityFramework.Application.Core;

namespace UtilityFramework.Application.Core.ViewModels
{
    public class BaseAddressViewModel
    {
        public BaseAddressViewModel()
        {
            FormatedAddress = Utilities.FormatAddress(StreetAddress, Number, Complement, Neighborhood, CityName, StateUf, ZipCode);
        }
        /// <summary>
        /// Cep
        /// </summary>
        [Display(Name = "Cep")]
        public string ZipCode { get; set; }
        /// <summary>
        /// Rua
        /// </summary>
        [Display(Name = "Rua")]
        public string StreetAddress { get; set; }
        /// <summary>
        /// Número
        /// </summary>
        [Display(Name = "Número")]
        public string Number { get; set; }
        /// <summary>
        /// Nome da Cidade
        /// </summary>
        [Display(Name = "Nome da Cidade")]
        public string CityName { get; set; }
        /// <summary>
        /// Identificador da cidade
        /// </summary>
        [Display(Name = "Identificador da cidade")]
        public string CityId { get; set; }
        /// <summary>
        /// Nome do Estado
        /// </summary>
        [Display(Name = "Nome do Estado")]
        public string StateName { get; set; }
        /// <summary>
        /// Uf do Estado
        /// </summary>
        [Display(Name = "Uf do Estado")]
        public string StateUf { get; set; }
        /// <summary>
        /// Identificador do estado
        /// </summary>
        [Display(Name = "Identificador do estado")]
        public string StateId { get; set; }
        /// <summary>
        /// Bairro
        /// </summary>
        [Display(Name = "Bairro")]
        public string Neighborhood { get; set; }
        /// <summary>
        /// Complemento
        /// </summary>
        [Display(Name = "Complemento")]
        public string Complement { get; set; }
        /// <summary>
        /// Gia
        /// </summary>
        [Display(Name = "Gia")]
        public string Gia { get; set; }
        /// <summary>
        /// Código do Ibge
        /// </summary>
        [Display(Name = "Complemento")]
        public string Ibge { get; set; }
        /// <summary>
        /// Endereço formatado
        /// </summary>
        [Display(Name = "Endereço formatado")]
        public string FormatedAddress { get; private set; }

        public void UpdateFormatedAddress()
        {
            FormatedAddress = Utilities.FormatAddress(StreetAddress, Number, Complement, Neighborhood, CityName, StateUf, ZipCode);
        }



    }
}
