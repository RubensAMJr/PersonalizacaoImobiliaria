using MediatR;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using System.Security.Claims;

namespace PersonalizacaoImobiliaria.Application.Handlers;

public class CadastrarPersonalizacaoCommandHandler : IRequestHandler<PersonalizacaoCommandRequest, PersonalizacaoCommandResult>
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CadastrarPersonalizacaoCommandHandler(IUnidadeDataStore unidadeDataStore, IHttpContextAccessor httpContextAccessor)
    {
        _unidadeDataStore = unidadeDataStore;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<PersonalizacaoCommandResult> Handle(PersonalizacaoCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var commandValidation = request.Validate();
            if (!string.IsNullOrEmpty(commandValidation))
                throw new ArgumentException(commandValidation);

            var personalizacaoExistente = await _unidadeDataStore.GetPersonalizacaoPorNome(request.Nome);
            if (personalizacaoExistente != null && personalizacaoExistente != Guid.Empty)
                throw new ArgumentException($"Já existe uma personalização com o nome {request.Nome}. ");

            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var personalizacaoId = await _unidadeDataStore.CadastrarPersonalizacao(new Personalizacao
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                Descricao = request.Descricao,
                Tipo = request.Tipo,
                Valor = request.Valor,
                UsuarioId = Guid.Parse(identity.FindFirst("IdUsuario").Value),
                UsuarioNome = identity.FindFirst("NomeUsuario").Value,
            }
            );

            return new PersonalizacaoCommandResult { Id = personalizacaoId };
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ocorreu um erro ao processar a solicitação: {ex.Message}", ex);
        }
    }
}
