//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Management.Sql.Fluent;
using System.Collections.Generic;

namespace AzureSqlMapper
{
    public interface ICollectAzureAssests
    {
        List<AzureSqlProps> GetSqlProps(IEnumerable<ISqlServer> sqlServers);
    }
}