//using DocumentFormat.OpenXml.Bibliography;

namespace AzureSqlMapper
{
    public class SqlFirewallRule
    {
        public string SqlServerName { get; set; }
        public string Kind { get; set; }
        public string StartIPAddress { get; set; }
        public string ParentId { get; set; }
        public string EndIPAddress { get; set; }
    }
}
