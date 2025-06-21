using NSubstitute;
using PersonalizacaoImobiliaria.Application.DTO;
using PersonalizacaoImobiliaria.Application.Handlers;
using PersonalizacaoImobiliaria.Application.Interfaces;
using PersonalizacaoImobiliaria.Application.Queries;
using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliariaTest.Tests.Handlers;

public class ListarSolicitacoesQueryHandlerTest
{
    private readonly ISolicitacaoDataStore _solicitacaoDataStore;
    private readonly ListarSolicitacoesQueryHandler _handler;

    public ListarSolicitacoesQueryHandlerTest()
    {
        _solicitacaoDataStore = Substitute.For<ISolicitacaoDataStore>();
        _handler = new ListarSolicitacoesQueryHandler(_solicitacaoDataStore);
    }

    [Fact]
    public async Task ListarSolicitacoesSucessTest()
    {
        // Arrange
        var request = new ListarSolicitacoesQueryRequest();

        var solicitacoes = new List<SolicitacoesDTO>
        {
            new SolicitacoesDTO
            {
                UnidadeId = Guid.NewGuid(),
                NomeUnidade = "Unidade teste 1",
                NomeCliente = "Cliente teste 1",
                Personalizacoes = new List<Personalizacao>(),
                ValorTotal = 690.0m,
                Status = EStatusSolicitacao.EmAnalise,
                DataSolicitacao = DateTime.UtcNow
            }
        };

        _solicitacaoDataStore.ListarSolicitacoes(null).Returns(solicitacoes);

        // Act
        var result = await _handler.Handle(request, default);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Solicitacoes);
        Assert.Equal("Unidade teste 1", result.Solicitacoes[0].NomeUnidade);
    }
}