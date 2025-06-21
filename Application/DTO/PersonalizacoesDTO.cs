using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.DTO;

public class PersonalizacoesDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public ETipoPersonalizacao Tipo { get; set; }
    public decimal Valor { get; set; }
    public Guid UserId { get; set; }
}
