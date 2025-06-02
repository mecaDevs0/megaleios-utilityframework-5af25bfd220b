using System.Collections.Generic;

namespace UtilityFramework.Application.Core.ViewModels
{
    public class LogViewModel
    {
        public string Url { get; set; }
        public string Method { get; set; }
        public object Body { get; set; }
        public string Form { get; set; }
        public string QueryString { get; set; }
        public string ParamterType { get; set; }
        public object Headers { get; set; }
        public object Response { get; set; }
        public int StatusCode { get; set; }
    }
}