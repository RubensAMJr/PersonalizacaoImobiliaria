using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Queries;
using System.Net;

namespace PersonalizacaoImobiliaria.Infrastructure.Controllers;

[Route("unidade")]
[Produces("application/json")]
[ApiController]
public class UnidadeController : ControllerBase
{
    private readonly IMediator _mediator;

    public UnidadeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Cadastro de uma Unidade vendida.
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [Route("")]
    [ProducesResponseType(typeof(UnidadeVendidaCommandResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RequestCadastrarUnidadeVendida([FromBody] UnidadeVendidaCommandRequest commandRequest)
    {
        try
        {
            var commandResult = await _mediator.Send(commandRequest);
            return Ok(commandResult);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Cadastro de uma Personalização imobiliaria
    /// </summary>
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [Route("personalizacao")]
    [ProducesResponseType(typeof(PersonalizacaoCommandResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RequestCadastrarPersonalizacao([FromBody] PersonalizacaoCommandRequest commandRequest)
    {
        try
        {
            var commandResult = await _mediator.Send(commandRequest);
            return Ok(commandResult);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Listagem de Personalizações disponíveis
    /// </summary>
    [HttpGet]
    [Authorize]
    [Route("personalizacao/listar")]
    [ProducesResponseType(typeof(ListarPersonalizacoesQueryResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RequestListarPersonalizacoes([FromQuery] ListarPersonalizacoesQueryRequest queryRequest)
    {
        try
        {
            var queryResult = await _mediator.Send(queryRequest);
            return Ok(queryResult);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}
