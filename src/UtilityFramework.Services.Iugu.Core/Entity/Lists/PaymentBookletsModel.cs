using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Entity.Lists
{
    public class PaymentBookletsModel : IuguBaseErrors
    {

        [JsonProperty("payment_booklets")]
        public List<IuguPaymentBookletModel> PaymentBooklets { get; set; } = new List<IuguPaymentBookletModel>();
    }
}