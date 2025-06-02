namespace UtilityFramework.Services.Stripe.Core3.Models
{
    public class StripeTransferRequest
    {
        /// <summary>
        /// Id da subconta que irá receber o saldo
        /// </summary>
        /// <value></value>
        public string SubAccountId { get; set; }
        /// <summary>
        /// Valor em centavos
        /// </summary>
        /// <value></value>
        public double Amount { get; set; }
        /// <summary>
        /// Código de moeda ISO de três letras representando a moeda padrão para a conta.
        /// Esta deve ser uma moeda que a Stripe suporta no país da conta.
        /// </summary>
        /// <value>BRL</value>
        public string Currency { get; set; } = "BRL";
        /// <summary>
        /// Você pode usar este parâmetro para transferir fundos de uma cobrança antes que eles sejam adicionados
        /// ao seu saldo disponível. Um saldo pendente será transferido imediatamente, mas os
        /// fundos não ficarão disponíveis até que a cobrança original se torne disponível.
        /// Consulte a documentação do Connect para mais detalhes.
        /// Ou informe o ID de uma subconta para transferencia entre subcontas
        /// </summary>
        /// <value></value>
        public string SourceTransactionId { get; set; }
        /// <summary>
        /// Descrição
        /// </summary>
        /// <value></value>
        public string Description { get; set; }
    }
}