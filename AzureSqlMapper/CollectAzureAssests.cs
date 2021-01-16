//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Management.Sql.Fluent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureSqlMapper
{
    partial class Program
    {
        public class CollectAzureAssests : ICollectAzureAssests
        {
            public List<AzureSqlProps> GetSqlProps(IEnumerable<ISqlServer> sqlServers)
            {
                List<AzureSqlProps> _sqllist = new List<AzureSqlProps>();

                

                foreach (var sqlServer in sqlServers)
                {
                    AzureSqlProps sQLProps = new AzureSqlProps();

                    sQLProps.FullyQualifiedDomainName = sqlServer.FullyQualifiedDomainName;
                    sQLProps.AdministratorLogin = sqlServer.AdministratorLogin;
                    sQLProps.IsManagedServiceIdentityEnabled = sqlServer.IsManagedServiceIdentityEnabled;
                    sQLProps.Kind = sqlServer.Kind;
                    sQLProps.State = sqlServer.State;
                    sQLProps.SystemAssignedManagedServiceIdentityPrincipalId = sqlServer.SystemAssignedManagedServiceIdentityPrincipalId;
                    sQLProps.SystemAssignedManagedServiceIdentityTenantId = sqlServer.SystemAssignedManagedServiceIdentityTenantId;
                    sQLProps.Version = sqlServer.Version;
                   // sQLProps.Id = sqlServer.Id;
                    sQLProps.TenantId = sqlServer.SystemAssignedManagedServiceIdentityTenantId;
                    sQLProps.RegionName = sqlServer.RegionName;
                    sQLProps.ResourceGroupName = sqlServer.ResourceGroupName;
                    sQLProps.AdministratorLogin = sqlServer.AdministratorLogin;
                    sQLProps.AdministratorLoginPassword = sqlServer.Inner.AdministratorLoginPassword;
                    sQLProps.Name = sqlServer.Name;
                    sQLProps.Key = sqlServer.Key;


                   // Binding firewall Rules

                    var _firewallRules = sqlServer.FirewallRules.List();

                    foreach (var item in _firewallRules)
                    {
                        SqlFirewallRule _sqlFirewallRule = new SqlFirewallRule();

                        _sqlFirewallRule.ParentId = item.ParentId;
                        _sqlFirewallRule.SqlServerName = item.SqlServerName;
                        _sqlFirewallRule.StartIPAddress = item.StartIPAddress;
                        _sqlFirewallRule.EndIPAddress = item.EndIPAddress;
                        _sqlFirewallRule.Kind = item.Kind;

                        sQLProps.SqlFirewallRule.Add(_sqlFirewallRule);

                    }

                    // Binding the sql Encryption protector

                    var _sqlEncryptionProtectors = sqlServer.EncryptionProtectors.List();

                    foreach (var item in _sqlEncryptionProtectors)
                    {
                        SqlEncryptionProtector _sqlEncryptionProtector = new SqlEncryptionProtector();

                        _sqlEncryptionProtector.Kind = item.Kind;
                        _sqlEncryptionProtector.ParentId = item.ParentId;
                        _sqlEncryptionProtector.ServerKeyName = item.ServerKeyName;
                        _sqlEncryptionProtector.SqlServerName = item.SqlServerName;
                        _sqlEncryptionProtector.Thumbprint = item.Thumbprint;

                        sQLProps.sqlEncryptionProtectors.Add(_sqlEncryptionProtector);
                    }

                    var _sqlVirtualNetworkRules = sqlServer.VirtualNetworkRules.List();


                    _sqllist.Add(sQLProps);
                }

                return _sqllist;
            }
        }
    }
}
