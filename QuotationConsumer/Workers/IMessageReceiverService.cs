namespace QuotationConsumer.Workers;

public interface IMessageReceiverService
{
    Task StartListening();
}