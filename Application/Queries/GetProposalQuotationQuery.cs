using Core.Models;
using MediatR;

namespace Application.Queries;

public record GetProposalQuotationQuery(int id) : IRequest<ProposalQuotationDto> {}