using System.Collections.Generic;
using MongoDB.Driver;

namespace UtilityFramework.Infra.Core3.MongoDb.Data.Server.Interface
{
    public interface IConfigurationServer
    {
        /// <summary>
        /// Main server or proxy
        /// </summary>
        MongoServerAddress Server { get; set; }

        /// <summary>
        /// Alternative destinations
        /// </summary>
        List<MongoServerAddress> Servers { get; set; }
    }
}