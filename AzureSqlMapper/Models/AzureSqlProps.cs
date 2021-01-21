
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace AzureSqlMapper.Models
{
    public class AzureSqlProps
    {
        public AzureSqlProps()
        {
            SqlFirewallRule = new List<SqlFirewallRule>();
            sqlEncryptionProtectors = new List<SqlEncryptionProtector>();
            sqlVirtualNetworkRules = new List<SqlVirtualNetworkRule>();

        }
        [BsonId]
        public Guid Id { get; set; }
        public string SystemAssignedManagedServiceIdentityTenantId { get; set; }
        public string Version { get; set; }
        public string SystemAssignedManagedServiceIdentityPrincipalId { get; set; }
        public string Kind { get; set; }
        public string FullyQualifiedDomainName { get; set; }
        public bool IsManagedServiceIdentityEnabled { get; set; }
        public string AdministratorLogin { get; set; }
        public string State { get; set; }

        public string Type { get; set; }
        public string RegionName { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string ResourceGroupName { get; set; }
        public string TenantId { get; set; }
        public string AdministratorLoginPassword { get; set; }
        public DateTime RecordAdded  {  get; set; }

        public List<SqlFirewallRule> SqlFirewallRule;
        public List<SqlEncryptionProtector> sqlEncryptionProtectors;
        public List<SqlVirtualNetworkRule> sqlVirtualNetworkRules;

    }
}
