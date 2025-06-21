using MediatR;
using System.Text;

namespace PersonalizacaoImobiliaria.Application.Commands;

public class SolicitacaoCommandRequest : IRequest<SolicitacaoCommandResult>
{
    public Guid UnidadeId { get; set; }
    public List<Guid> PersonalizacoesId { get; set; }

    public string Validate()
    {
        var errors = new StringBuilder();

        if (UnidadeId == Guid.Empty)
            errors.Append("A unidade n�o pode ser vazia. ");

        if (PersonalizacoesId == null || PersonalizacoesId.Count == 0)
            errors.Append("A solicita��o deve conter ao menos uma personaliza��o ");

        return errors.ToString();
    }
}