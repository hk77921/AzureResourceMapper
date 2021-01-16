//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Core;
using Microsoft.Azure.Management.Sql.Fluent;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace AzureSqlMapper
{
    public class AzureAssets : IAzureAssets
    {

        //string clientId = "564a986e-c252-429f-948d-32dd9ee08b43";
        //string clientSecret = "QRBD~kb1eyVR~09K7.fiC_tVb~~mGfPmSR";
        //string tenantId = "184e4ab4-a9bf-4b68-a72f-eb4e03b54ae1";

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
