using Microsoft.AspNetCore.Http;
using NSubstitute;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Handlers;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PersonalizacaoImobiliariaTest.Tests.Handlers;

public class CadastrarUnidadeVendidaCommandHandlerTest
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CadastrarUnidadeVendidaCommandHandler _handler;

    public CadastrarUnidadeVendidaCommandHandlerTest()
    {
        _unidadeDataStore = Substitute.For<IUnidadeDataStore>();
        _httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        _handler = new CadastrarUnidadeVendidaCommandHandler(_unidadeDataStore, _httpContextAccessor);

        var identidadeMock = new List<Claim> { new Claim("IdUsuario", Guid.NewGuid().ToString()), new Claim("NomeUsuario", "Usuario Teste") };
        _httpContextAccessor.HttpContext?.User.Identity.Returns(new ClaimsIdentity(identidadeMock));
    }

    [Fact]
    public async Task CadastrarUnidadeVendidaSucessTest()
    {
        // Arrange
        var request = new UnidadeVendidaCommandRequest
        {
            Nome = "Unidade 1",
            NumeroUnidade = 11,
            NomeCliente = "Cliente Teste",
            CpfCliente = "12345678900"
        };

        _unidadeDataStore.CadastrarUnidadeVendida(Arg.Any<Unidade>()).Returns(Guid.NewGuid());

        // Act
        var result = await _handler.Handle(request, new CancellationToken());

        // Assert
        Assert.NotNull(result);
        Assert.NotEqual(Guid.Empty, result.Id);
    }
}
