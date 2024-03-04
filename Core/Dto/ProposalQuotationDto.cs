using Core.Enums;

namespace Core.Models;

public class ProposalQuotationDto
{
    public int Id { get; set; }
    public CodeType Tipo { get; set; }
    public object Cotacao { get; set; }
    public Status Status { get; set; }
    public decimal Valor { get; set; }
}