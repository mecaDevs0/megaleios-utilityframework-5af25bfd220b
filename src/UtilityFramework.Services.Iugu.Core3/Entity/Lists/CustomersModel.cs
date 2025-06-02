using System.Collections.Generic;
using UtilityFramework.Services.Iugu.Core3.Entity;

namespace UtilityFramework.Services.Iugu.Core3.Entity.Lists
{
    public class CustomersModel
    {
        public int totalItems { get; set; }
        public List<CustomerModel> items { get; set; }
    }
}
