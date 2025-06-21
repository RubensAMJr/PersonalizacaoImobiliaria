using Microsoft.AspNetCore.Http;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Handlers;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;
using System.Security.Claims;

namespace PersonalizacaoImobiliariaTest.Tests.Handlers;

public class CadastrarSolicitacaoCommandHandlerTest
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly ISolicitacaoDataStore _solicitacaoDataStore;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CadastrarSolicitacaoCommandHandler _handler;

    public CadastrarSolicitacaoCommandHandlerTest()
    {
        _unidadeDataStore = Substitute.For<IUnidadeDataStore>();
        _solicitacaoDataStore = Substitute.For<ISolicitacaoDataStore>();
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _handler = new CadastrarSolicitacaoCommandHandler(_unidadeDataStore, _solicitacaoDataStore, _httpContextAccessor);

        var identidadeMock = new List<Claim> { new Claim("IdUsuario", Guid.NewGuid().ToString()), new Claim("NomeUsuario", "Usuario Teste") };
        _httpContextAccessor.HttpContext?.User.Identity.Returns(new ClaimsIdentity(identidadeMock));
    }

    [Fact]
    public async Task CadastrarSolicitacaoSucessTest()
    {
        // Arrange
        var unidadeId = Guid.NewGuid();
        var personalizacaoId1 = Guid.NewGuid();
        var personalizacaoId2 = Guid.NewGuid();
        var request = new SolicitacaoCommandRequest
        {
            UnidadeId = unidadeId,
            PersonalizacoesId = new List<Guid> { personalizacaoId1, personalizacaoId2 }
        };

        var unidade = new Unidade
        {
            Id = unidadeId,
            Nome = "Unidade 1",
            NumeroUnidade = 101,
            NomeCliente = "Cliente Teste",
            CpfCliente = "123.654.456-45"
        };

        var personalizacoes = new List<Personalizacao>
        {
            new Personalizacao
            {
                Id = personalizacaoId1,
                Nome = "Personalização 1",
                Descricao = "Descrição teste 1",
                Tipo = ETipoPersonalizacao.Cor,
                Valor = 500.0m
            },
            new Personalizacao
            {
                Id = personalizacaoId2,
                Nome = "Personalização 2",
                Descricao = "Descrição teste 2",
                Tipo = ETipoPersonalizacao.Estrutura,
                Valor = 200.0m
            }
        };

        _unidadeDataStore.GetUnidadePorId(unidadeId).Returns(unidade);
        _solicitacaoDataStore.GetSolicitacaoByUnidade(unidadeId).ReturnsNull();
        _unidadeDataStore.ListarPersonalizacoesPorId(request.PersonalizacoesId).Returns(personalizacoes);
        _solicitacaoDataStore.CadastrarSolicitacao(Arg.Any<Solicitacao>()).Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(request, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task CadastrarSolicitacaoUnidadeInexistenteTest()
    {
        // Arrange
        var unidadeId = Guid.NewGuid();
        var personalizacaoId1 = Guid.NewGuid();
        var personalizacaoId2 = Guid.NewGuid();
        var request = new SolicitacaoCommandRequest
        {
            UnidadeId = unidadeId,
            PersonalizacoesId = new List<Guid> { personalizacaoId1, personalizacaoId2 }
        };

        _unidadeDataStore.GetUnidadePorId(unidadeId).ReturnsNull();

        // Assert
        var exception = await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(request, new CancellationToken()));
        Assert.Equal($"Ocorreu um erro ao processar a solicitação: Unidade com o ID informado {request.UnidadeId} não foi encontrada.", exception.Message);
    }

    [Fact]
    public async Task CadastrarSolicitacaoJaExistenteTest()
    {
        // Arrange
        var unidadeId = Guid.NewGuid();
        var personalizacaoId1 = Guid.NewGuid();
        var personalizacaoId2 = Guid.NewGuid();
        var request = new SolicitacaoCommandRequest
        {
            UnidadeId = unidadeId,
            PersonalizacoesId = new List<Guid> { personalizacaoId1, personalizacaoId2 }
        };

        var unidade = new Unidade
        {
            Id = unidadeId,
            Nome = "Unidade 1",
            NumeroUnidade = 101,
            NomeCliente = "Cliente Teste",
            CpfCliente = "12365445645"
        };

        _unidadeDataStore.GetUnidadePorId(unidadeId).Returns(unidade);
        _solicitacaoDataStore.GetSolicitacaoByUnidade(unidadeId).Returns(unidade.Nome);

        // Assert
        var exception = await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(request, new CancellationToken()));
        Assert.Equal($"Ocorreu um erro ao processar a solicitação: Uma solicitação para a Unidade {unidade.Nome} já existe.", exception.Message);
    }

    [Fact]
    public async Task CadastrarSolicitacaoPersonalizacoesNaoExistentesTest()
    {
        // Arrange
        var unidadeId = Guid.NewGuid();
        var personalizacaoId1 = Guid.NewGuid();
        var personalizacaoId2 = Guid.NewGuid();
        var request = new SolicitacaoCommandRequest
        {
            UnidadeId = unidadeId,
            PersonalizacoesId = new List<Guid> { personalizacaoId1, personalizacaoId2 }
        };

        var unidade = new Unidade
        {
            Id = unidadeId,
            Nome = "Unidade 1",
            NumeroUnidade = 101,
            NomeCliente = "Cliente Teste",
            CpfCliente = "123.654.456-45"
        };

        _unidadeDataStore.GetUnidadePorId(unidadeId).Returns(unidade);
        _solicitacaoDataStore.GetSolicitacaoByUnidade(unidadeId).ReturnsNull();
        _unidadeDataStore.ListarPersonalizacoesPorId(request.PersonalizacoesId).ReturnsNull();

        // Assert
        var exception = await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(request, new CancellationToken()));
        Assert.Equal($"Ocorreu um erro ao processar a solicitação: Nenhuma personalização foi encontrada com os IDs informados.", exception.Message);
    }
}