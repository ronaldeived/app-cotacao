using Application.Validators;
using Core.Enums;
using MediatR;
using QuotationProducer.Workers;
using Repository.Service;

namespace Application.Commands;

public class HouseQuotationGenerateCommandHandler : IRequestHandler<HouseQuotationGenerateCommand, int>
{
    private readonly MongoDBService _repository;
    private readonly IProducerMessage _producerMessage;

    public HouseQuotationGenerateCommandHandler(MongoDBService repository, IProducerMessage producerMessage)
    {
        _repository = repository;
        _producerMessage = producerMessage;
    }

    public async Task<int> Handle(HouseQuotationGenerateCommand request, CancellationToken cancellationToken)
    {
        if (!AgeValidator.LegalAge(request.dto.Segurado.DataNascimento))
            throw new Exception("É preciso ter 18 anos ou mais para fazer uma cotação");
        
        var id = await _repository.CreateAsync(request.dto, Status.Processing, CodeType.House);
        
        await _producerMessage.SendMessage("quotation", "house-quotation",id.ToString());

        return id;
    }
}