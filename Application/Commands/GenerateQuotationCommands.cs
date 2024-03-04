using Core.Models;
using MediatR;

namespace Application.Commands;

public record CarQuotationGenerateCommand(QuotationBaseDto<CarDto> dto) : IRequest<int> {}
public record HouseQuotationGenerateCommand(QuotationBaseDto<HouseDto> dto) : IRequest<int> {}
