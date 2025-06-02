using Microsoft.Extensions.Hosting;
using System;
using UtilityFramework.Infra.Core3.MongoDb.Data.Database.Interface;

namespace UtilityFramework.Infra.Core3.MongoDb.Data.Database
{
    public class DataBaseConfig : IConfiguration
    {
        private readonly BaseSettings _baseSettings;
        public DataBaseConfig(IHostingEnvironment env)
        {
            _baseSettings = AppSettingsBase.GetSettings(env);
        }


        public string DataBaseName
        {
            get => _baseSettings.Name;
            set => throw new NotImplementedException();
        }

    }
}