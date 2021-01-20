using System;
using Microsoft.Extensions.DependencyInjection;

namespace TempDI
{
    public class serviceA : IserviceA
    {
        private readonly IserviceB _serviceB;

        public serviceA(IserviceB serviceB)
        {
            Console.WriteLine("Service A started ..");
            _serviceB = serviceB;
        }
        public void SomeAWork()
        {
            _serviceB.ServiceBWork();
        }

    }
}
