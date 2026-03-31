using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartaoCreditoValido.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CartaoCreditoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CartaoCreditoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CriarCartaoCreditoRequest request, CancellationToken cancellationToken)
    {
        var command = new CriarCartaoCreditoCommand(
            request.NomeCompletoTitular,
            request.NascimentoTitular,
            request.NumeroCartao);

        var resultado = await _mediator.Send(command, cancellationToken);
        
        if(resultado == null)
            return BadRequest();
        
        return CreatedAtAction(nameof(ObterPorId), new { id = resultado.Id }, resultado);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(ObterCartaoCreditoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id, CancellationToken cancellationToken)
    {
        var query = new ObterCartaoCreditoPorIdQuery(id);
        var resultado = await _mediator.Send(query, cancellationToken);
        
        if(resultado == null)
            return NotFound();
        
        return Ok(resultado);
    }
}
