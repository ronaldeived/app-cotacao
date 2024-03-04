using System.Text;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace QuotationConsumer.Workers;

public class HouseConsumer : IMessageReceiverService
{
    private IModel _channel;
    private MessageModule _messageModule = new MessageModule();
    private readonly IMediator _mediator;
    
    public HouseConsumer(IMediator mediator)
    {
        _channel = _messageModule.GetChannel();
        _channel.ExchangeDeclare(exchange: "house-quotation", type: ExchangeType.Direct);
        _channel.QueueDeclare(queue: "quotation",
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
        _channel.QueueBind(queue: "quotation", exchange: "house-quotation", routingKey: "quotation");
        
        _mediator = mediator;
    }
    
    public Task StartListening()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            try
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                _channel.BasicAck(ea.DeliveryTag, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _channel.BasicNack(ea.DeliveryTag, false, true);
            }
        };
        
        _channel.BasicConsume(queue: "quotation",
            autoAck: false,
            consumer: consumer);
        
        return Task.CompletedTask;
    }
}