using Microsoft.Extensions.Configuration;
using UtilityFramework.Infra.Core3.MongoDb.Data.Server; // Usando a classe concreta

namespace UtilityFramework.Infra.Core3.MongoDb.Data.Database
{
    // REMOVEMOS a implementação ": IDataAccess"
    public class DataAccess 
    {
        private readonly IConfiguration _configuration;
        private readonly DataBaseConfig _dataBaseConfig;
        // Usamos a CLASSE CONCRETA "ServerAccess" em vez da interface
        private readonly ServerAccess _serverAccess; 

        public DataAccess(IConfiguration configuration)
        {
            _configuration = configuration;
            _dataBaseConfig = new DataBaseConfig(_configuration);
            // Criamos a CLASSE CONCRETA "ServerAccess"
            _serverAccess = new ServerAccess(_configuration); 
        }

        public DataBaseConfig GetDataBaseConfig()
        {
            return _dataBaseConfig;
        }

        public string GetDataBaseName()
        {
            return _dataBaseConfig.DatabaseName;
        }

        public string GetConnectionString()
        {
            return _dataBaseConfig.ConnectionString;
        }

        // O método agora retorna a CLASSE CONCRETA "ServerAccess"
        public ServerAccess GetServerAccess() 
        {
            return _serverAccess;
        }
    }
}