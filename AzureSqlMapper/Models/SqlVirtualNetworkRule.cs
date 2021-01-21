//using DocumentFormat.OpenXml.Bibliography;

namespace AzureSqlMapper.Models
{
    public class SqlVirtualNetworkRule
    {

        public string SubnetId { get; set; }
        public string SqlServerName { get; set; }
        public string State { get; set; }
        public string ParentId { get; set; }
    }
}
