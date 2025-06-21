using MediatR;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using System.Security.Claims;

namespace PersonalizacaoImobiliaria.Application.Handlers;

public class CadastrarUnidadeVendidaCommandHandler : IRequestHandler<UnidadeVendidaCommandRequest, UnidadeVendidaCommandResult>
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CadastrarUnidadeVendidaCommandHandler(IUnidadeDataStore unidadeDataStore, IHttpContextAccessor httpContextAccessor)
    {
        _unidadeDataStore = unidadeDataStore;
        _httpContextAccessor = httpContextAccessor;
    }
    public async Task<UnidadeVendidaCommandResult> Handle(UnidadeVendidaCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var commandValidation = request.Validate();
            if (!string.IsNullOrEmpty(request.Validate()))
                throw new ArgumentException(commandValidation);

            var identity = _httpContextAccessor.HttpContext?.User.Identity as ClaimsIdentity;
            var unidadeId = await _unidadeDataStore.CadastrarUnidadeVendida(new Unidade
            {
                Id = Guid.NewGuid(),
                Nome = request.Nome,
                NomeCliente = request.NomeCliente,
                CpfCliente = request.CpfCliente,
                NumeroUnidade = request.NumeroUnidade,
                UsuarioId = Guid.Parse(identity.FindFirst("IdUsuario").Value),
                UsuarioNome = identity.FindFirst("NomeUsuario").Value,
            });
            return new UnidadeVendidaCommandResult { Id = unidadeId };
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"Ocorreu um erro ao processar a solicitação: {ex.Message}", ex);
        }
    }
}
