using Microsoft.Extensions.DependencyInjection;
using QuotationConsumer.Workers;

namespace QuotationConsumer.DI;

public static class ConsumerDI
{
    public static IServiceCollection AddConsumerDIs(this IServiceCollection service)
    {
        service
            .AddSingleton<MessageModule>()
            .AddScoped<IMessageReceiverService, CarConsumer>();
            
        return service;
    }
}