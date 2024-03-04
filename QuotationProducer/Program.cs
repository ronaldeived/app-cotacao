using Microsoft.Extensions.DependencyInjection;
using QuotationProducer.DI;

namespace QuotationConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddProducerDIs()
                .BuildServiceProvider();
            
            Console.ReadKey();
        }
    }
}