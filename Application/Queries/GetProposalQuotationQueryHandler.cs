using Core.Models;
using MediatR;
using Repository.Service;

namespace Application.Queries;

public class GetProposalQuotationQueryHandler : IRequestHandler<GetProposalQuotationQuery, ProposalQuotationDto>
{
    private readonly MongoDBService _repository;

    public GetProposalQuotationQueryHandler(MongoDBService repository)
    {
        _repository = repository;
    }

    public async Task<ProposalQuotationDto<obk> Handle(GetProposalQuotationQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetById(request.id);

        return result;
    }
}