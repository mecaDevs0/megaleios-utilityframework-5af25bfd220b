using System.Collections.Generic;

namespace UtilityFramework.Application.Core3.ViewModels
{
    public class AddressViewModel
    {
        /// <summary>
        /// ENDEREÇO FORMATADO
        /// </summary>
        public string FormatedAddress { get; set; }
        /// <summary>
        /// RUA
        /// </summary>
        public string Street { get; set; }
        /// <summary>
        /// NUMERO
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// ESTADO
        /// </summary>
        public string State { get; set; }
        /// <summary>
        /// CIDADE
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// PAIS
        /// </summary>
        public string Country { get; set; }
        /// <summary>
        /// CEP
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// BAIRROS
        /// </summary>
        public string Neighborhood { get; set; }
        /// <summary>
        /// BAIRRO
        /// </summary>
        public Geometry Geometry { get; set; }
        /// <summary>
        /// INFORMA SE OCORREU UM ERRO
        /// </summary>
        public bool Erro { get; set; }
        /// <summary>
        /// MENSAGEM DE ERRO
        /// </summary>
        ///
        public string ErroMessage { get; set; }
        /// <summary>
        /// Componentes do endereço
        /// </summary>
        public List<AddressComponents> AddressComponents { get; set; }
    }
}