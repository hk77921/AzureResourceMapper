using System.Collections.Generic;
using MongoDB.Driver;
using MongoDB.Bson;
using AzureSqlMapper.Repository;
using AzureSqlMapper.Models;
using Microsoft.Extensions.Configuration;

namespace AzureSqlMapper.Respository
{
    public class MongoDBInit : DatabaseSettings
    {
        private IConfiguration _config;


        public MongoDBInit(IConfiguration config)
        {
            _config = config;
            if (_config != null)
            {
                this.ConnectionString = _config.GetValue<string>("DatabaseSettings:ConnectionString");
                this.DatabaseName = _config.GetValue<string>("DatabaseSettings:DatabaseName");
            }
        }

    }
}
