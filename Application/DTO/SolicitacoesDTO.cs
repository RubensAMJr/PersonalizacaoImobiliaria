using PersonalizacaoImobiliaria.Domain.Entities;
using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.DTO;

public class SolicitacoesDTO
{
    public Guid UnidadeId { get; set; }
    public string NomeUnidade { get; set; }
    public string NomeCliente { get; set; }
    public List<Personalizacao> Personalizacoes { get; set; }
    public decimal ValorTotal { get; set; }
    public EStatusSolicitacao Status { get; set; }
    public DateTime DataSolicitacao { get; set; }
}
