using MediatR;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;
using System.Security.Claims;

namespace PersonalizacaoImobiliaria.Application.Handlers;

public class CadastrarSolicitacaoCommandHandler : IRequestHandler<SolicitacaoCommandRequest, SolicitacaoCommandResult>
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly ISolicitacaoDataStore _solicitacaoDataStore;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CadastrarSolicitacaoCommandHandler(IUnidadeDataStore unidadeDataStore, ISolicitacaoDataStore solicitacaoDataStore, IHttpContextAccessor httpContextAccessor)
    {
        _unidadeDataStore = unidadeDataStore;
        _solicitacaoDataStore = solicitacaoDataStore;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<SolicitacaoCommandResult> Handle(SolicitacaoCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var commandValidation = request.Validate();
            if (!string.IsNullOrEmpty(commandValidation))
                throw new ArgumentException(commandValidation);

            var unidade = await _unidadeDataStore.GetUnidadePorId(request.UnidadeId);
            if (unidade == null)
                throw new KeyNotFoundException($"Unidade com o ID informado {request.UnidadeId} não foi encontrada.");

            var unidadeSolicitacao = await _solicitacaoDataStore.GetSolicitacaoByUnidade(request.UnidadeId);
            if (unidadeSolicitacao != null)
                throw new InvalidOperationException($"Uma solicitação para a Unidade {unidadeSolicitacao} já existe.");

            var personalizacoes = await _unidadeDataStore.ListarPersonalizacoesPorId(request.PersonalizacoesId);
            if (personalizacoes == null)
                throw new KeyNotFoundException($"Nenhuma personalização foi encontrada com os IDs informados.");

            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var solicitacaoId = await _solicitacaoDataStore.CadastrarSolicitacao(new Solicitacao
            {
                Id = Guid.NewGuid(),
                Unidade = unidade,
                Personalizacoes = personalizacoes,
                Status = EStatusSolicitacao.EmAnalise,
                ValorTotal = personalizacoes.Sum(p => p.Valor),
                DataCriacao = DateTime.UtcNow,
                UsuarioId = Guid.Parse(identity.FindFirst("IdUsuario").Value),
                UsuarioNome = identity.FindFirst("NomeUsuario").Value,
            }
            );
            return new SolicitacaoCommandResult { Id = solicitacaoId };
        }
        catch(Exception ex)
        {
            throw new ApplicationException($"Ocorreu um erro ao processar a solicitação: {ex.Message}", ex);
        }
    }
}