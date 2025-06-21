using Microsoft.AspNetCore.Mvc;

namespace PersonalizacaoImobiliaria.Application.Commands;


public class PersonalizacaoCommandResult : ActionResult
{
    public Guid Id { get; set; }
}
