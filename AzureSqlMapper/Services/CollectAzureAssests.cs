using AzureSqlMapper.Models;
using Microsoft.Azure.Management.Sql.Fluent;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AzureSqlMapper.Services
{
    public class CollectAzureAssests : ICollectAzureAssests
    {
        public List<AzureSqlProps> GetSqlProps(IEnumerable<ISqlServer> sqlServers)
        {

            List<AzureSqlProps> _sqllist = new List<AzureSqlProps>();

            Log.Information("Collecting SQL properties");

            foreach (var sqlServer in sqlServers)
            {
                Console.WriteLine(Environment.NewLine);
                Log.Information("Collecting SQL server {0}", sqlServer.Name);

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
                sQLProps.RecordAdded = DateTime.Now;


                Task.Run(() => FirewallRules(sqlServer, sQLProps)).GetAwaiter().GetResult();

                Task.Run(() => Encryptionprotector(sqlServer, sQLProps)).GetAwaiter().GetResult();

                Task.Run(() => SqlFailoverGroup(sqlServer, sQLProps)).GetAwaiter().GetResult();

                Task.Run(() => SqlServerDnsAliases(sqlServer, sQLProps)).GetAwaiter().GetResult();

                Task.Run(() => SqlDatabases(sqlServer, sQLProps)).GetAwaiter().GetResult();
               
                Task.Run(() => SqlVirtualNetworkRules(sqlServer, sQLProps)).GetAwaiter().GetResult();

                _sqllist.Add(sQLProps);
            }

            return _sqllist;
        }

        private void Encryptionprotector(ISqlServer sqlServer, AzureSqlProps sQLProps)
        {
            Log.Information("Collecting Encryptionprotector for {0}", sqlServer.Name);

            var _sqlEncryptionProtectors = sqlServer.EncryptionProtectors.List();

            Log.Information("{0} Encryptionprotector for server {1}", _sqlEncryptionProtectors.Count, sqlServer.Name);

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
        }

        private void FirewallRules(ISqlServer sqlServer, AzureSqlProps sQLProps)
        {
            Log.Information("Collecting FirewallRules for {0}", sqlServer.Name);

            var _firewallRules = sqlServer.FirewallRules.List();

            Log.Information("{0} FirewallRules for server {1}", _firewallRules.Count, sqlServer.Name);

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

        }

        private void SqlFailoverGroup(ISqlServer sqlServer, AzureSqlProps sQLProps)
        {
            Log.Information("Collecting SqlFailoverGroup for {0}", sqlServer.Name);

            var _sqlFailoverGroups = sqlServer.FailoverGroups.List();

            Log.Information("{0} SqlFailoverGroup for server {1}", _sqlFailoverGroups.Count, sqlServer.Name);

            foreach (var item in _sqlFailoverGroups)
            {
                SqlFailoverGroup _sqlFailoverGroup = new SqlFailoverGroup();

                _sqlFailoverGroup.ParentId = item.ParentId;
                // _sqlFailoverGroup.Databases = item.Databases;
                _sqlFailoverGroup.ReadWriteEndpointDataLossGracePeriodMinutes = item.ReadWriteEndpointDataLossGracePeriodMinutes;
                _sqlFailoverGroup.ReplicationState = item.ReplicationState;
                _sqlFailoverGroup.SqlServerName = item.SqlServerName;


                sQLProps.sqlFailoverGroups.Add(_sqlFailoverGroup);

            }

        }

        private void SqlServerDnsAliases(ISqlServer sqlServer, AzureSqlProps sQLProps)
        {
            Log.Information("Collecting SqlServerDnsAliases for {0}", sqlServer.Name);

            var _sqlServerDnsAliases = sqlServer.DnsAliases.List();

            Log.Information("{0} SqlServerDnsAliases for server {1}", _sqlServerDnsAliases.Count, sqlServer.Name);

            foreach (var item in _sqlServerDnsAliases)
            {
                SqlServerDnsAlias _sqlServerDnsAlias = new SqlServerDnsAlias();

                _sqlServerDnsAlias.ParentId = item.ParentId;
                _sqlServerDnsAlias.AzureDnsRecord = item.AzureDnsRecord;
                _sqlServerDnsAlias.SqlServerName = item.SqlServerName;
                sQLProps.sqlServerDnsAliases.Add(_sqlServerDnsAlias);

            }

        }

        private void SqlDatabases(ISqlServer sqlServer, AzureSqlProps sQLProps)
        {
            Log.Information("Collecting SqlDatabases for {0}", sqlServer.Name);

            var _sqlServerDatabases = sqlServer.Databases.List();

            Log.Information("{0} SqlDatabases for server {1}", _sqlServerDatabases.Count, sqlServer.Name);

            foreach (var item in _sqlServerDatabases)
            {
                SqlDatabase _sqlServerDatabase = new SqlDatabase();

                _sqlServerDatabase.ParentId = item.ParentId;
                _sqlServerDatabase.SqlServerName = item.SqlServerName;
                _sqlServerDatabase.Collation = item.Collation;
                _sqlServerDatabase.CreationDate = item.CreationDate;
                _sqlServerDatabase.CurrentServiceObjectiveId = item.CurrentServiceObjectiveId;
                _sqlServerDatabase.DatabaseId = item.DatabaseId;
                _sqlServerDatabase.DefaultSecondaryLocation = item.DefaultSecondaryLocation;
                _sqlServerDatabase.EarliestRestoreDate = item.EarliestRestoreDate;
                _sqlServerDatabase.Edition = item.Edition.Value;
                _sqlServerDatabase.ElasticPoolName = item.ElasticPoolName;
                _sqlServerDatabase.IsDataWarehouse = item.IsDataWarehouse;
                _sqlServerDatabase.MaxSizeBytes = item.MaxSizeBytes;
                _sqlServerDatabase.RegionName = item.RegionName;

                // _sqlServerDatabase.Tags = item.Tags.Values;

                sQLProps.sqlDatabases.Add(_sqlServerDatabase);

            }

        }

        private void SqlVirtualNetworkRules(ISqlServer sqlServer, AzureSqlProps sQLProps)
        {
            Log.Information("Collecting SqlVirtualNetworkRule for {0}", sqlServer.Name);

            var _sqlVirtualNetworkRules = sqlServer.VirtualNetworkRules.List();

            Log.Information("{0} SqlVirtualNetworkRule for server {1}", _sqlVirtualNetworkRules.Count, sqlServer.Name);

            foreach (var item in _sqlVirtualNetworkRules)
            {
                SqlVirtualNetworkRule _sqlVirtualNetworkRule = new SqlVirtualNetworkRule();

                _sqlVirtualNetworkRule.ParentId = item.ParentId;
                _sqlVirtualNetworkRule.SqlServerName = item.SqlServerName;
                _sqlVirtualNetworkRule.SubnetId = item.SubnetId;
                _sqlVirtualNetworkRule.State = item.State;

                sQLProps.sqlVirtualNetworkRules.Add(_sqlVirtualNetworkRule);

            }

        }
    }
}

