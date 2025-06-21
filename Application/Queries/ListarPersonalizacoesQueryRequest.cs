using MediatR;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.Queries;

public class ListarPersonalizacoesQueryRequest : IRequest<ListarPersonalizacoesQueryResult>
{
    public ETipoPersonalizacao? TipoPersonalizacao { get; set; }
}
