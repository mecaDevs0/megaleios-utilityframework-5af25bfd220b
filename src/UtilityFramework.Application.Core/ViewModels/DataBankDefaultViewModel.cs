using System.ComponentModel.DataAnnotations;
using UtilityFramework.Application.Core;

namespace UtilityFramework.Application.Core.ViewModels
{
    public class DataBankDefaultViewModel : BaseViewModel

    {
        [Required(ErrorMessage = ValidationMessageBase.RequiredField)]
        [Display(Name = "Nome do Responsável")]
        public string AccountableName { get; set; }

        [Required(ErrorMessage = ValidationMessageBase.RequiredField)]
        [Display(Name = "Cpf do Responsável")]
        public string AccountableCpf { get; set; }

        [Required(ErrorMessage = ValidationMessageBase.RequiredField + " no formato ########-#")]
        [Display(Name = "Conta")]
        public string BankAccount { get; set; }

        [Required(ErrorMessage = ValidationMessageBase.RequiredField + " no formato ####-#, obs: digito é opcional")]
        [Display(Name = "Agencia")]
        public string BankAgency { get; set; }

        [Required(ErrorMessage = ValidationMessageBase.RequiredField)]
        [Display(Name = "Banco")]
        public string Bank { get; set; }

        [Required(ErrorMessage = ValidationMessageBase.RequiredField)]
        [Display(Name = "Tipo de conta")]
        public string TypeAccount { get; set; }


        [Required(ErrorMessage = ValidationMessageBase.RequiredField)]
        [Display(Name = "Tipo de pessoa")]
        public string PersonType { get; set; }
        public string Cnpj { get; set; }
    }
}