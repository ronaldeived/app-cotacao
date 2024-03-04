using API.Validations;
using Application.Commands;
using Application.Queries;
using Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
// ReSharper disable All

namespace API.Controllers;

[ApiController]
[Route("api/cotacao")]
public class CotacaoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CotacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]

    public async Task<IActionResult> Get(int id)
    {
        var result = await _mediator.Send(new GetProposalQuotationQuery(id));
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] QuotationBaseDto<object>? quotation)
    {
        if (quotation == null) return BadRequest("Payload inválido");

        if (DeserializeCar(quotation.Item) is CarDto car && Validation.IsCar(car))
        {
            if (!Validation.ValidCar(car))
                return BadRequest("Chassi, Placa e modelo são requeridos");

            
            var id = await _mediator.Send(
                new CarQuotationGenerateCommand(MapToCarQuotation(quotation, car))
            );
            
            return Ok(id);
        }
        else if (DeserializeHouse(quotation.Item) is HouseDto house && Validation.IsHouse(house))
        {
            if (!Validation.ValidHouse(house))
                return BadRequest("Beneficiário e endereço são requeridos");
            
            var id = await _mediator.Send(
                new HouseQuotationGenerateCommand(MapToHouseQuotation(quotation, house))
            );
            
            return Ok(id);
        }
        else
        {
            return BadRequest("Payload Inválido");
        }
    }

    private static CarDto DeserializeCar(object? item)
    {
        return JsonConvert.DeserializeObject<CarDto>(item.ToString());
    }

    private static HouseDto DeserializeHouse(object item)
    {
        return JsonConvert.DeserializeObject<HouseDto>
            (item.ToString().Replace("endereço", "endereco"));
    }

    private static QuotationBaseDto<CarDto> MapToCarQuotation(QuotationBaseDto<object> quotation, CarDto car)
    {
        return new QuotationBaseDto<CarDto>
        {
            Segurado = quotation.Segurado,
            Item = car
        };
    }
    
    private static QuotationBaseDto<HouseDto> MapToHouseQuotation(QuotationBaseDto<object> quotation, HouseDto house)
    {
        return new QuotationBaseDto<HouseDto>
        {
            Segurado = quotation.Segurado,
            Item = house
        };
    }
}