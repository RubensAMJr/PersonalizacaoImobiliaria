using MediatR;
using PersonalizacaoImobiliaria.Domain.Enums;
using System.Text;

namespace PersonalizacaoImobiliaria.Application.Commands;

public class PersonalizacaoCommandRequest : IRequest<PersonalizacaoCommandResult>
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public ETipoPersonalizacao Tipo { get; set; }
    public decimal Valor { get; set; }

    public string Validate()
    {
        var errors = new StringBuilder();

        if (string.IsNullOrWhiteSpace(Nome))
            errors.Append("O nome da Personalização não pode ser vazio. ");

        if((int)Tipo == 0)
            errors.Append("O tipo da Personalização é invalido. ");

        if(Valor <= 0)
            errors.Append("O valor da Personalização náo pode ser menor que zero. ");

        return errors.ToString();
    }
}
