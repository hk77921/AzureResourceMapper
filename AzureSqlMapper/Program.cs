using System;
using System.Threading.Tasks;
//using DocumentFormat.OpenXml.Bibliography;
using Microsoft.Azure.Management.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent;
using Microsoft.Azure.Management.ResourceManager.Fluent.Authentication;
using Microsoft.Azure.Management.Sql.Fluent;
using Microsoft.Azure.Management.Sql.Fluent.Models;
using Azure.Identity;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using System.IO;
using Serilog;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AzureSqlMapper
{
    partial class Program
    {
        static void Main(string[] args)
        {

            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);


            Log.Logger = new LoggerConfiguration()
                         .ReadFrom.Configuration(builder.Build())
                         .Enrich.FromLogContext()
                         .WriteTo.Console()
                         .CreateLogger();

            Log.Logger.Information("Application is starting..");

            var host = Host.CreateDefaultBuilder()
                        .ConfigureServices((context, services) =>
                        {
                            // services.AddTransient<IMyClass, MyClass>();
                            services.AddScoped<IAzureAssets, AzureAssets>();
                            services.AddScoped<ICollectAzureAssests, CollectAzureAssests>();
                        })
                        .UseSerilog()
                        .Build();
            Log.Logger.Information("Application is getting ready..");

            static void BuildConfiguration(IConfigurationBuilder builder)
            {
                builder.SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                        .AddJsonFile($"appsetting{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production"}.json", optional: true)
                        .AddEnvironmentVariables();

            }

            Log.Logger.Information("Setting up the environment");

            var svc = ActivatorUtilities.CreateInstance<AzureAssets>(host.Services);
            var temp = ActivatorUtilities.CreateInstance<CollectAzureAssests>(host.Services);

            var sqlServers = svc.GetSqlServers();
            Log.Logger.Information("Collecting the sqlservers");

            //temp.GetSqlProps(sqlServers);

            Log.Logger.Information("Collecting the sqlservers properties");

            MongoCRUD db = new MongoCRUD("AzureMapper01");
            Log.Logger.Information("Conneting to database");



            //PrintData(temp.GetSqlProps(sqlServers));
            db.InsertRecords("SQLFirewall", temp.GetSqlProps(sqlServers));
            Log.Logger.Information("Pushing to database");
            //var temp=  db.LoadRecords<AzureSqlProps>("SQLFirewall");
            //var temp1 = db.FindRecoredByID<AzureSqlProps>("SQLFirewall", "singlepanetest.database.windows.net");


            Console.WriteLine("waiting for user to click to stop!!");

            Console.ReadKey();
        }
        private static void PrintData(List<AzureSqlProps> _sqllist)
        {
            foreach (var item in _sqllist)
            {

                Console.WriteLine($"FullyQualifiedDomainName:{item.FullyQualifiedDomainName}");
                Console.WriteLine($"AdministratorLogin:{item.AdministratorLogin}");
                Console.WriteLine($"IsManagedServiceIdentityEnabled:{item.IsManagedServiceIdentityEnabled}");
                Console.WriteLine($"Kind:{item.Kind}");
                Console.WriteLine($"State:{item.State}");
                Console.WriteLine($"SystemAssignedManagedServiceIdentityPrincipalId:{item.SystemAssignedManagedServiceIdentityPrincipalId}");
                Console.WriteLine($"SystemAssignedManagedServiceIdentityTenantId:{item.SystemAssignedManagedServiceIdentityTenantId}");
                Console.WriteLine($"Version:{item.Version}");

                Console.WriteLine($"Id:{item.Id}");
                Console.WriteLine($"TenantId:{item.TenantId}");
                Console.WriteLine($"RegionName:{item.RegionName}");
                Console.WriteLine($"ResourceGroupName:{item.ResourceGroupName}");
                Console.WriteLine($"AdministratorLogin:{item.AdministratorLogin}");
                Console.WriteLine($"AdministratorLoginPassword:{item.AdministratorLoginPassword}");
                Console.WriteLine($"Name:{item.Name}");
                Console.WriteLine($"Key:{item.Key}");

                Console.WriteLine("***************************************");

                Console.WriteLine($"********Printing FireWall Rules for id : {item.Id}**********");

                foreach (var rule in item.SqlFirewallRule)
                {

                    Console.WriteLine($"\t\t ParentId:{rule.ParentId}");
                    Console.WriteLine($"\t\t SqlServerName:{rule.SqlServerName}");
                    Console.WriteLine($"\t\t StartIPAddress:{rule.StartIPAddress}");
                    Console.WriteLine($"\t\t EndIPAddress:{rule.EndIPAddress}");
                    Console.WriteLine($"\t\t Kind:{rule.Kind}");

                }


            }
        }
    }
}
