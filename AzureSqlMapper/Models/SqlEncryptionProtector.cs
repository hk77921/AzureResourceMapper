//using DocumentFormat.OpenXml.Bibliography;

namespace AzureSqlMapper.Models
{
    public class SqlEncryptionProtector
    {
        public string ServerKeyName { get; set; }
        public string SqlServerName { get; set; }
        public string Kind { get; set; }
        public string Thumbprint { get; set; }
        public string Uri { get; set; }
        public string ParentId { get; set; }
    }
}
