using UtilityFramework.Services.Iugu.Core.Models;

namespace UtilityFramework.Services.Iugu.Core.Entity
{
    public class IuguBaseCustomer : IuguBaseErrors
    {
        public string AccountKey { get; set; }
        public string AccountKeyDev { get; set; }
    }
}