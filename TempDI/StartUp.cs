using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;


namespace TempDI
{
    public class StartUp
    {
        private readonly IConfiguration _config;
        private readonly IserviceA iserviceA;

        public StartUp(IConfiguration config,IserviceA iserviceA)
        {
            _config = config;
            this.iserviceA = iserviceA;
        }
     
        public void Run()
        {
            Console.WriteLine("Welcome from startup..");

            var logDirectory = _config.GetValue<string>("Runtime:LogOutputDirectory");
            // Using serilog here, can be anything
            var log = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File(logDirectory)
                .CreateLogger();

            log.Information("Serilog logger information");
            Console.WriteLine("Hello from App.cs");
          
        }
      

       
    }
}
