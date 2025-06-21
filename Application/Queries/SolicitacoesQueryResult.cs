using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.Queries;

public class SolicitacoesQueryResult
{
    public Guid UnidadeId { get; set; }
    public string NomeUnidade { get; set; }
    public string NomeCliente { get; set; }
    public List<Personalizacao> Personalizacoes { get; set; }
    public decimal ValorTotal { get; set; }
    public EStatusSolicitacao Status { get; set; }
    public string DataSolicitacao { get; set; }
}

