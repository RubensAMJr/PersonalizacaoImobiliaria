using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalizacaoImobiliaria.Application.Commands;
using PersonalizacaoImobiliaria.Application.Queries;
using System.Net;

namespace PersonalizacaoImobiliaria.Infrastructure.Controllers;

[Route("solicitacao")]
[Produces("application/json")]
[ApiController]
public class SolicitacaoController : ControllerBase
{
    private readonly IMediator _mediator;

    public SolicitacaoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Solicitação de uma Personalização para uma Unidade imobiliaria
    /// </summary>
    [HttpPost]
    [Authorize]
    [Route("")]
    [ProducesResponseType(typeof(SolicitacaoCommandResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RequestSolicitarPersonalizacao([FromBody] SolicitacaoCommandRequest commandRequest)
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
    /// Listagem de solicitações das Unidades imobiliarias
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Administrador")]
    [Route("listar")]
    [ProducesResponseType(typeof(ListarSolicitacoesQueryResult), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> RequestListarSolicitacoes([FromQuery] ListarSolicitacoesQueryRequest queryRequest)
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