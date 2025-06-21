using MediatR;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace PersonalizacaoImobiliaria.Application.Handlers
{
    public class ListarSolicitacoesQueryHandler : IRequestHandler<ListarSolicitacoesQueryRequest, ListarSolicitacoesQueryResult>
    {
        private readonly ISolicitacaoDataStore _solicitacaoDataStore;

        public ListarSolicitacoesQueryHandler(ISolicitacaoDataStore solicitacaoDataStore)
        {
            _solicitacaoDataStore = solicitacaoDataStore;
        }

        public async Task<ListarSolicitacoesQueryResult> Handle(ListarSolicitacoesQueryRequest request, CancellationToken cancellationToken)
        {
            var solicitacoes = await _solicitacaoDataStore.ListarSolicitacoes(request.TipoOrdenacao);
            return new ListarSolicitacoesQueryResult
            {
                Solicitacoes = solicitacoes.Select(s => new SolicitacoesQueryResult
                {
                    UnidadeId = s.UnidadeId,
                    NomeUnidade = s.NomeUnidade,
                    NomeCliente = s.NomeCliente,
                    Personalizacoes = s.Personalizacoes,
                    ValorTotal = s.ValorTotal,
                    Status = s.Status,
                    DataSolicitacao = s.DataSolicitacao.ToString("dd/MM/yyyy HH:mm")    
                }).ToList()
            };
        }
    }
}