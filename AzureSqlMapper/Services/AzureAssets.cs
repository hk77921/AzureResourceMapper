//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace AzureSqlMapper.Services
{
    public class AzureAssets : IAzureAssets
    {

        protected string clientId { get; private set; }
        protected string clientSecret { get; private set; }
        protected string tenantId { get; private set; }


        private readonly IConfiguration _config;

        public AzureAssets(IConfiguration config)
        {
            _config = config;
            clientId = _config.GetValue<string>("AzureSecret:clientId");
            clientSecret = _config.GetValue<string>("AzureSecret:clientSecret");
            tenantId = _config.GetValue<string>("AzureSecret:tenantId");

        }
        public IEnumerable<ISqlServer> GetSqlServers()
        {
            var credentials = SdkContext.AzureCredentialsFactory.
                FromServicePrincipal(clientId, clientSecret, tenantId, AzureEnvironment.AzureGlobalCloud);

            var azure = Microsoft.Azure.Management.Fluent.Azure.Configure().
                WithLogLevel(HttpLoggingDelegatingHandler.Level.Basic).
                Authenticate(credentials).WithDefaultSubscription();

            var sqlServers = azure.SqlServers.List();
            return sqlServers;
        }
    }
       
}
