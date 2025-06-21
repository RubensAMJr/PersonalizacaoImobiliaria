using Microsoft.AspNetCore.Mvc;

namespace PersonalizacaoImobiliaria.Application.Commands;

public class UnidadeVendidaCommandResult : ActionResult
{
    public Guid Id { get; set; }
}
