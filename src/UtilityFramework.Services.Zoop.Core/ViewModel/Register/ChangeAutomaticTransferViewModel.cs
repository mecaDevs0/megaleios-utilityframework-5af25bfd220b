using Newtonsoft.Json;
using UtilityFramework.Services.Zoop.Core.Enum;
using UtilityFramework.Services.Zoop.Core.ViewModel.Response;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class ChangeAutomaticTransferViewModel : BaseErrorViewModel
    {
        public ChangeAutomaticTransferViewModel()
        {
            TransferEnabled = true;
            MinimumTransferValue = 1;
        }

        /// <summary>
        /// DETERMINA INTERVALO DE TRANSFERENCIAS
        /// </summary>
        /// <value></value>
        [JsonProperty("transfer_interval")]
        public TransferInterval TransferInterval { get; set; }

        /// <summary>
        /// DETERMINA O DIA QUE DEVE SER REALIZADA A TRANSFERENCIA 
        /// </summary>
        /// <value></value>
        [JsonProperty("transfer_day")]
        public int? TransferDay { get; set; }

        /// <summary>
        /// DETERMINA SE A TRANSFERENCIA SERÁ FEITA AUTOMATICAMENTE OU MANUALMENTE (CASO DESABILITADO VIA API NÃO TEMOS MAIS SUPORTE PELA ZOOP SOMENTE VIA API)
        /// </summary>
        /// <value></value>
        [JsonProperty("transfer_enabled")]
        public bool TransferEnabled { get; set; }
        /// <summary>
        /// VALOR MINIMO DE TRANSFERENCIA 
        /// </summary>
        /// <value></value>
        [JsonProperty("minimum_transfer_value")]
        public int MinimumTransferValue { get; set; }

    }
}