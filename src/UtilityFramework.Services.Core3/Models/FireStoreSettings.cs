namespace UtilityFramework.Services.Core3.Models
{
    public class FireStoreSettings
    {

        public FireStoreSettings()
        {
            Environment = "GOOGLE_APPLICATION_CREDENTIALS";
        }
        public string FileCredentials { get; set; }
        public string ProjectId { get; set; }
        public string Environment { get; }


    }
}