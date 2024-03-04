using Microsoft.Extensions.DependencyInjection;
using QuotationProducer.Workers;

namespace QuotationProducer.DI;

public static class ProducerDI
{
    public static IServiceCollection AddProducerDIs(this IServiceCollection service)
    {
        service
            .AddSingleton<MessageModule>()
            .AddScoped<IProducerMessage, ProducerMessage>();
            
        return service;
    }
}