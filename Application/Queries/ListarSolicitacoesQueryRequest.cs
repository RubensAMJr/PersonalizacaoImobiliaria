using MediatR;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.Queries;

public class ListarSolicitacoesQueryRequest : IRequest<ListarSolicitacoesQueryResult>
{
    public ETipoOrdenacaoSolicitacoes? TipoOrdenacao { get; set; }
}