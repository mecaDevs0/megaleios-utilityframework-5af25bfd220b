namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeTransferDataPartnerModel
    {
        /// <summary>
        /// ID da conta Stripe do parceiro
        /// </summary>
        public string AccountId { get; set; }

        /// <summary>
        /// Comissão do parceiro (valor fixo ou porcentagem)
        /// Se não informado, assume o valor restante após a comissão da plataforma
        /// </summary>
        public double? PartnerCommission { get; set; }

        /// <summary>
        /// Tipo de cálculo para a comissão do parceiro
        /// </summary>
        public EStripeTypeValue PartnerCommissionType { get; set; }

    }
}