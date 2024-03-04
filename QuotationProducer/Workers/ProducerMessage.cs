using System.Text;
using RabbitMQ.Client;

namespace QuotationProducer.Workers;

public class ProducerMessage : IProducerMessage
{
    private IModel _channel;
    private MessageModule _messageModule = new MessageModule();
    
    public Task SendMessage(string queueName, string exchange, string message)
    {
        _channel = _messageModule.GetChannel();
        _channel.QueueDeclare(queue: queueName,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null);

        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: exchange,
            routingKey: queueName,
            basicProperties: null,
            body: body);
        
        return Task.CompletedTask;
    }
}