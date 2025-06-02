namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripePaymentIntentSplitModel
    {
        /// <summary>
        /// Comissão da plataforma (valor fixo ou porcentagem)
        /// Se não informado, assume o valor restante após a comissão do parceiro
        /// </summary>
        public double? PlatformCommission { get; set; }

        /// <summary>
        /// Tipo de cálculo para a comissão da plataforma
        /// </summary>
        public EStripeTypeValue PlatformCommissionType { get; set; }

        /// <summary>
        /// Configurações de transferência para o parceiro/subconta
        /// Se não informado, toda a transação fica com a plataforma
        /// </summary>
        public StripeTransferDataPartnerModel PartnerTransfer { get; set; }

    }


}