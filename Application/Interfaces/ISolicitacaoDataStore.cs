using PersonalizacaoImobiliaria.Application.DTO;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.Interfaces;

public interface ISolicitacaoDataStore
{
    Task<Guid> CadastrarSolicitacao(Solicitacao solicitacao);
    Task<List<SolicitacoesDTO>> ListarSolicitacoes(ETipoOrdenacaoSolicitacoes? tipoOrdenacaoSolicitacoes);
    Task<string?> GetSolicitacaoByUnidade(Guid unidadeId);
}
