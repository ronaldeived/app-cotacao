namespace QuotationProducer.Workers;

public interface IProducerMessage
{
    Task SendMessage(string queueName, string exchange, string message);
}