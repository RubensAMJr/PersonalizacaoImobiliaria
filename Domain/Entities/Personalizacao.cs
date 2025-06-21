using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Domain.Entities;

public class Personalizacao
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public ETipoPersonalizacao Tipo { get; set; }
    public decimal Valor { get; set; }
    public Guid UsuarioId { get; set; }
    public string UsuarioNome { get; set; }
    public List<Solicitacao> Solicitacoes { get; set; } = [];
}
