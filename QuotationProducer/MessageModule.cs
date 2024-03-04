using RabbitMQ.Client;

namespace QuotationProducer;

public class MessageModule
{
    private readonly ConnectionFactory _factory;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public MessageModule()
    {
        _factory = new ConnectionFactory() { HostName = "localhost" }; 
        _connection = _factory.CreateConnection();
        _channel = _connection.CreateModel();
    }
    
    public IModel GetChannel()
    {
        return _channel;
    }

    public void CloseConnection()
    {
        _connection.Close();
    }
}