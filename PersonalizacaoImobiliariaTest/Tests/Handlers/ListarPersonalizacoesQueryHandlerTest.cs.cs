using NSubstitute;
using PersonalizacaoImobiliaria.Application.DTO;
using PersonalizacaoImobiliaria.Application.Handlers;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Application.Queries;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliariaTest.Tests.Handlers;

public class ListarPersonalizacoesQueryHandlerTest
{
    private readonly IUnidadeDataStore _unidadeDataStore;
    private readonly ListarPersonalizacoesQueryHandler _handler;

    public ListarPersonalizacoesQueryHandlerTest()
    {
        _unidadeDataStore = Substitute.For<IUnidadeDataStore>();
        _handler = new ListarPersonalizacoesQueryHandler(_unidadeDataStore);
    }

    [Fact]
    public async Task ListarPersonalizacoesSucessTest()
    {
        // Arrange
        var tipoPersonalizacao = ETipoPersonalizacao.Cor;
        var request = new ListarPersonalizacoesQueryRequest
        {
            TipoPersonalizacao = tipoPersonalizacao
        };

        var personalizacoes = new List<PersonalizacoesDTO>
        {
            new PersonalizacoesDTO
            {
                Id = Guid.NewGuid(),
                Nome = "Personalização teste",
                Descricao = "Descrição teste",
                Tipo = tipoPersonalizacao,
                Valor = 750.0m,
                UserId = Guid.NewGuid()
            }
        };

        _unidadeDataStore.ListarPersonalizacoes(tipoPersonalizacao).Returns(personalizacoes);

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Personalizacoes);
        Assert.Equal("Personalização teste", result.Personalizacoes[0].Nome);
        Assert.Equal(750.0m, result.Personalizacoes[0].Valor);
    }
}