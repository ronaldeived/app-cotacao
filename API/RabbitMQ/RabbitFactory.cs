using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace API.RabbitMQ;

public class RabbitFactory
{
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private const string _exchange = "app-cotacao"; 

    public RabbitFactory()
    {
        _factory = new ConnectionFactory() { HostName = "localhost" }; 
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
        _channel.QueueDeclare(queue: "cotacao", durable: false, exclusive: false, autoDelete: false, arguments: null);
    }
    
    public async Task SendMessage(string message)
    {
        var body = Encoding.UTF8.GetBytes(message);
        _channel.BasicPublish(exchange: _exchange, routingKey: "cotacao-updated", basicProperties: null, body: body);
        await Task.CompletedTask;
    }

    public void ListenForMessages()
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine(" [x] Received {0}", message);
        };

        _channel.BasicConsume(queue: "cotacao", autoAck: true, consumer: consumer);
    }
}