using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Domain.Entities;

public class Solicitacao
{
    public Guid Id { get; set; }
    public Unidade Unidade { get; set; }
    public List<Personalizacao> Personalizacoes { get; set; } = [];
    public EStatusSolicitacao Status { get; set; }
    public decimal ValorTotal { get; set; }
    public DateTime DataCriacao { get; set; }
    public Guid UsuarioId { get; set; }
    public string UsuarioNome { get; set; }
}
