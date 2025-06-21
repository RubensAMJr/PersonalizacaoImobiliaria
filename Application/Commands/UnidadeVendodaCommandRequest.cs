using MediatR;
using System.Text;

namespace PersonalizacaoImobiliaria.Application.Commands;

public class UnidadeVendidaCommandRequest : IRequest<UnidadeVendidaCommandResult>
{
    public string Nome { get; set; }
    public int NumeroUnidade { get; set; }
    public string NomeCliente { get; set; }
    public string CpfCliente { get; set; }

    public string Validate()
    {
        var errors = new StringBuilder();

        if (string.IsNullOrWhiteSpace(Nome))
            errors.Append("O nome da Unidade não pode ser vazio. ");

        if (string.IsNullOrWhiteSpace(NomeCliente))
            errors.Append("O nome do Cliente não pode ser vazio.");

        if (string.IsNullOrWhiteSpace(CpfCliente))
            errors.Append("O CPF do Cliente não pode ser vazio.");

        if (CpfCliente.Length != 11)
            errors.Append("O CPF é invalido");

        return errors.ToString();
    }
}
