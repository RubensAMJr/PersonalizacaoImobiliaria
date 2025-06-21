using MediatR;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Application.Queries;

namespace PersonalizacaoImobiliaria.Application.Handlers;

public class ListarPersonalizacoesQueryHandler : IRequestHandler<ListarPersonalizacoesQueryRequest, ListarPersonalizacoesQueryResult>
{
    private readonly IUnidadeDataStore _unidadeDataStore;

    public ListarPersonalizacoesQueryHandler(IUnidadeDataStore unidadeDataStore)
    {
        _unidadeDataStore = unidadeDataStore;
    }

    public async Task<ListarPersonalizacoesQueryResult> Handle(ListarPersonalizacoesQueryRequest request, CancellationToken cancellationToken)
    {
        var listaPersonalizacoes = await _unidadeDataStore.ListarPersonalizacoes(request.TipoPersonalizacao);

        return new ListarPersonalizacoesQueryResult { Personalizacoes = listaPersonalizacoes.Select(
                                                                         p => new PersonalizacaoQueryResult
                                                                         {
                                                                            Nome = p.Nome,
                                                                            Descricao = p.Descricao,
                                                                            Tipo = p.Tipo,
                                                                            Valor = p.Valor
                                                                         }).ToList()};
    }
}