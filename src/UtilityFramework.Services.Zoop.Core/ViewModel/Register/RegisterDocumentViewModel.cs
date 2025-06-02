using Microsoft.AspNetCore.Http;
using UtilityFramework.Application.Core.ViewModels;

namespace UtilityFramework.Services.Zoop.Core.ViewModel.Register
{
    public class RegisterDocumentViewModel : BaseViewModel
    {
        public string Category { get; set; }
        public string Metadata { get; set; }
        public string Description { get; set; }
    }
}