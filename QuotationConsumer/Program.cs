using Microsoft.Extensions.DependencyInjection;
using QuotationConsumer.DI;
using QuotationConsumer.Workers;

namespace QuotationConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceCollection()
                .AddConsumerDIs()
                .BuildServiceProvider();
            
            var consumer = serviceProvider.GetService<IMessageReceiverService>();
            
            consumer.StartListening();

            Console.ReadKey();
        }
    }
}