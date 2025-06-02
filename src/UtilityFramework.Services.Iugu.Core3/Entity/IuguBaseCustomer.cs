using UtilityFramework.Services.Iugu.Core3.Models;

namespace UtilityFramework.Services.Iugu.Core3.Entity
{
    public class IuguBaseCustomer : IuguBaseErrors
    {
        public string AccountKey { get; set; }
        public string AccountKeyDev { get; set; }
    }
}