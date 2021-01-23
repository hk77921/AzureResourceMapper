using System;
using System.Collections.Generic;
using System.Text;

namespace AzureSqlMapper.Models
{
    public class SqlFailoverGroup
    {
        public string ReplicationState { get; set; }
        public List<string> Databases { get; set; }
        public string SqlServerName { get; set; }
        public int ReadWriteEndpointDataLossGracePeriodMinutes { get; set; }
        public string ParentId { get; set; }
    }
}
