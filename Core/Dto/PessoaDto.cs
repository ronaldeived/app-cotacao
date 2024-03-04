namespace Core.Models;

public class PessoaDto
{
    public string Nome { get; set; }
}

public class PessoaFisicaDto : PessoaDto
{
    public decimal Cpf { get; set; }
    public DateTime DataNascimento { get; set; }
}

public class PessoaJuridicaDto : PessoaDto
{
    public decimal Cnpj { get; set; }
}