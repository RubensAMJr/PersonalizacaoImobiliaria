namespace PersonalizacaoImobiliaria.Domain.Entities;

public class Unidade
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int NumeroUnidade { get; set; }
    public string NomeCliente { get; set; }
    public string CpfCliente { get; set; }
    public Guid UsuarioId { get; set; }
    public string UsuarioNome { get; set; }
}
