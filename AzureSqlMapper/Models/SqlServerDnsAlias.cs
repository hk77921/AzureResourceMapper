using System;
using System.Collections.Generic;
using System.Text;

namespace AzureSqlMapper.Models
{
    public class SqlServerDnsAlias
    {
        public string SqlServerName { get; set; }
        public string AzureDnsRecord { get; set; }
        public string ParentId { get; set; }
    }
}
