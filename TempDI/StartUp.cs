using Microsoft.Extensions.Logging;

namespace TempDI
{
    public class StartUp
    {
        private readonly ILogger<StartUp> _logger;
        private readonly IserviceA _serviceA;

        public StartUp(ILogger<StartUp> logger, IserviceA serviceA)
        {
            _logger = logger;
            _serviceA = serviceA;

        }
        public void Run()
        {
            //_logger.LogInformation("Calling from startup..");
            System.Console.WriteLine("Calling from startup");
            _serviceA.SomeAWork();
        }
    }
}
