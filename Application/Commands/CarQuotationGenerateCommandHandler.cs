using Application.Validators;
using Core.Enums;
using MediatR;
using QuotationProducer.Workers;
using Repository.Service;

namespace Application.Commands;

public class CarQuotationGenerateCommandHandler : IRequestHandler<CarQuotationGenerateCommand, int>
{
    private readonly MongoDBService _repository;
    private readonly IProducerMessage _producerMessage;
    
    public CarQuotationGenerateCommandHandler(MongoDBService repository, IProducerMessage producerMessage)
    {
        _repository = repository;
        _producerMessage = producerMessage;
    }
    
    public async Task<int> Handle(CarQuotationGenerateCommand request, CancellationToken cancellationToken)
    {
        if (!AgeValidator.LegalAge(request.dto.Segurado.DataNascimento))
            throw new Exception("É preciso ter 18 anos ou mais para fazer uma cotação");
        
        var id = await _repository.CreateAsync(request.dto, Status.Processing, CodeType.Car);
        
        await _producerMessage.SendMessage("quotation", "car-quotation",id.ToString());

        return id;
    }
}