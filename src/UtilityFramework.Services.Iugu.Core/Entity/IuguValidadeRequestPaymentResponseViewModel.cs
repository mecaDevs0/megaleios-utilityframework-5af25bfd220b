using Newtonsoft.Json;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class IuguValidadeRequestPaymentResponseViewModel : IuguBasicResponseViewModel
    {

        [JsonProperty("payment_info")]
        public PaymentInfoResponseViewModel PaymentInfo { get; set; }
    }

}