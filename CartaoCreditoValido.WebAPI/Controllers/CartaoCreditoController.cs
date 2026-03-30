using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CartaoCreditoValido.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CartaoCreditoController : ControllerBase
{
    private readonly ISender _sender;

    public CartaoCreditoController(ISender sender)
    {
        _sender = sender;
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

        var resultado = await _sender.Send(command, cancellationToken);

        return CreatedAtAction(nameof(ObterPorId), new { id = resultado }, null);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ObterPorId(int id, CancellationToken cancellationToken)
    {
        var query = new ObterCartaoCreditoPorIdQuery(id);
        var resultado = await _sender.Send(query, cancellationToken);
        
        return Ok(resultado);
    }
}
