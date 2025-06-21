namespace PersonalizacaoImobiliaria.Application.DTO;

public class UnidadeVendidaDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public int NumeroUnidade { get; set; }
    public string NomeCliente { get; set; }
    public string CpfCliente { get; set; }
    public Guid UserId { get; set; }
}
