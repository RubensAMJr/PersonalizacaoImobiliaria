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

public class CadastrarPersonalizacaoCommandHandlerTest
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CadastrarPersonalizacaoCommandHandler _handler;

    public CadastrarPersonalizacaoCommandHandlerTest()
    {
        _unidadeDataStore = Substitute.For<IUnidadeDataStore>();
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _handler = new CadastrarPersonalizacaoCommandHandler(_unidadeDataStore, _httpContextAccessor);

        var identidadeMock = new List<Claim> { new Claim("IdUsuario", Guid.NewGuid().ToString()), new Claim("NomeUsuario", "Usuario Teste") };
        _httpContextAccessor.HttpContext?.User.Identity.Returns(new ClaimsIdentity(identidadeMock));
    }

    [Fact]
    public async Task CadastrarPersonalizacaoSucessTest()
    {
        // Arrange
        var request = new PersonalizacaoCommandRequest
        {
            Nome = "Personalização1",
            Descricao = "Descrição teste 1",
            Tipo = ETipoPersonalizacao.Acabamento,
            Valor = 150.0m
        };
        _httpContextAccessor.HttpContext?.User.Identity.Returns(new ClaimsIdentity(new List<Claim>
        {
            new Claim("IdUsuario", Guid.NewGuid().ToString()),
            new Claim("NomeUsuario", "Usuario Teste")
        }));

        _unidadeDataStore.GetPersonalizacaoPorNome(request.Nome).ReturnsNull();
        _unidadeDataStore.CadastrarPersonalizacao(Arg.Any<Personalizacao>()).Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(request, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }

    [Fact]
    public async Task CadastrarPersonalizacaoJaExistenteTest()
    {
        // Arrange
        var request = new PersonalizacaoCommandRequest
        {
            Nome = "Personalização1",
            Descricao = "Descrição teste 1",
            Tipo = ETipoPersonalizacao.Acabamento,
            Valor = 150.0m
        };
        _httpContextAccessor.HttpContext?.User.Identity.Returns(new ClaimsIdentity(new List<Claim>
        {
            new Claim("IdUsuario", Guid.NewGuid().ToString()),
            new Claim("NomeUsuario", "Usuario Teste")
        }));

        _unidadeDataStore.GetPersonalizacaoPorNome(request.Nome).Returns(Guid.NewGuid());

        // Assert
        await Assert.ThrowsAsync<ApplicationException>(() => _handler.Handle(request, new CancellationToken()));
    }
}