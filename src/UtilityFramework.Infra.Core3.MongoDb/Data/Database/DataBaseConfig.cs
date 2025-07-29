using Microsoft.Extensions.Configuration; // Adicionamos o using necessário

namespace UtilityFramework.Infra.Core3.MongoDb.Data.Database
{
    public class DataBaseConfig
    {
        private readonly IConfiguration _configuration;

        // Construtor que recebe a configuração
        public DataBaseConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Propriedade que lê a ConnectionString do appsettings.json
        public string ConnectionString
        {
            get { return _configuration["DATABASE:CONNECTION_STRING"]; }
        }

        // Propriedade que lê o nome do banco de dados do appsettings.json
        public string DatabaseName
        {
            get { return _configuration["DATABASE:NAME"]; }
        }
    }
}