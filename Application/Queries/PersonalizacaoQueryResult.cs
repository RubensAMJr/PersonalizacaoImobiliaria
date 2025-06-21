using PersonalizacaoImobiliaria.Domain.Enums;

namespace PersonalizacaoImobiliaria.Application.Queries;

public class PersonalizacaoQueryResult
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public ETipoPersonalizacao Tipo { get; set; }
    public decimal Valor { get; set; }
}
