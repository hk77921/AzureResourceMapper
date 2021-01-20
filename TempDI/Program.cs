using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace TempDI
{
    public class Program
    {
        private readonly ILogger<Program> _logger;
        private readonly serviceA _serviceA;
        private static IServiceProvider _serviceProvider;

        static void Main(string[] args)
        {
            // var host = CreateHostBuilder(args).Build();

            // host.Services.GetRequiredService<Program>().Run();


            RegisterServices();
            IServiceScope scope = _serviceProvider.CreateScope();
            scope.ServiceProvider.GetRequiredService<StartUp>().Run();
            DisposeServices();

        }




        public Program(ILogger<Program> logger, serviceA serviceA)
        {
            _logger = logger;
            _serviceA = serviceA;
        }

        //public void Run()
        //{
        //    _logger.LogInformation("Program is running..");
        //    _serviceA.SomeAWork();
        //    _logger.LogInformation("Process completed!!");
        //}

        //private static IHostBuilder CreateHostBuilder(string[] args)
        //{
        //    return Host.CreateDefaultBuilder(args)
        //        .ConfigureServices(services =>
        //       {
        //           services.AddTransient<Program>();
        //           services.AddTransient<serviceA>();
        //           services.AddTransient<serviceB>();
        //       });
        //}


        private static void RegisterServices()
        {
            var services = new ServiceCollection();
            services.AddSingleton<StartUp>();
            services.AddSingleton<IserviceA, serviceA>();
            services.AddSingleton<IserviceB,serviceB>();
            services.AddLogging();
           
            _serviceProvider = services.BuildServiceProvider(true);
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }
            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
