namespace Core.Models;

public class QuotationBaseDto<T>
{
    public PessoaFisicaDto Segurado { get; set; }
    public T Item { get; set; }
}