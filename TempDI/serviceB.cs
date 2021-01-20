using Microsoft.Extensions.Logging;
using System;

namespace TempDI
{
    public class serviceB : IserviceB
    {
        private readonly ILogger<serviceB> _logger;

        public serviceB(ILogger<serviceB> logger)
        {
            _logger = logger;
            Console.WriteLine("Service B started");
        }

        // public void ServiceBWork() => _logger.LogInformation("Servie B is working.");
        public void ServiceBWork() => Console.WriteLine("Servie B is working.");
    }
}
