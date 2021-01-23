using AzureSqlMapper.Models;
using AzureSqlMapper.Repository;
using AzureSqlMapper.Respository;
using AzureSqlMapper.Services;
using AzureSqlMapperRespository.AppService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.IO;

namespace AzureSqlMapper
{
    public class Program
    {
        static void Main(string[] args)
        {

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("*****Starting the application*******");

            var services = ConfigureServices();
            Log.Information("configure service");


            var serviceProvider = services.BuildServiceProvider();
            Log.Information("Build service provider");

            serviceProvider.GetRequiredService<AzureAssetMapper>().Run();
            Console.ReadKey();
        }

        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var config = LoadConfiguration();

            services.AddSingleton(config);
            services.AddTransient<AzureAssetMapper>();
            services.AddScoped<IAzureAssets, AzureAssets>();
            services.AddScoped<ICollectAzureAssests, CollectAzureAssests>();
            services.AddSingleton<SqlRepository>();
            services.AddSingleton<MongoDBInit>();
            services.AddScoped<IDatabaseSettings, DatabaseSettings>();

            services.AddLogging();

            return services;
        }

        public static IConfiguration LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsetting{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "production"}.json", optional: true)
                .AddEnvironmentVariables();


            return builder.Build();
        }


    }
}
