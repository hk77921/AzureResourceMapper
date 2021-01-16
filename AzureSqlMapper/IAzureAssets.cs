﻿//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Management.Sql.Fluent;
using System.Collections.Generic;

namespace AzureSqlMapper
{
    public interface IAzureAssets
    {

        IEnumerable<ISqlServer> GetSqlServers();
    }
}