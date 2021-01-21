//using DocumentFormat.OpenXml.Bibliography;
using AzureSqlMapper.Models;
using Microsoft.Azure.Management.Sql.Fluent;
using System.Collections.Generic;

namespace AzureSqlMapper.Services
{
    public interface ICollectAzureAssests
    {
        List<AzureSqlProps> GetSqlProps(IEnumerable<ISqlServer> sqlServers);
    }
}