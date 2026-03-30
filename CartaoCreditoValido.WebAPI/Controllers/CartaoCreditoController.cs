using CartaoCreditoValido.Application.Services;
using CartaoCreditoValido.Domain.CartoesCredito.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CartaoCreditoValido.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class CartaoCreditoController : ControllerBase
{
    private readonly ICartaoCreditoService _cartaoCreditoService;

    public CartaoCreditoController(ICartaoCreditoService cartaoCreditoService)
    {
        _cartaoCreditoService = cartaoCreditoService;
    }

    /// <summary>
    /// Cria e armazena um novo cartão de crédito
    /// </summary>
    /// <param name="cartaoCredito">Dados do cartão de crédito a ser criado</param>
    /// <returns>Status 201 Created se bem-sucedido</returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post([FromBody] CartaoCredito cartaoCredito)
    {
        // if (cartaoCredito == null)
        //     return BadRequest("CartãoCredito não pode ser nulo.");
        //
        // // await _cartaoCreditoService.Armazenar(cartaoCredito, CancellationToken.None);
        //
        // return CreatedAtAction(nameof(Post), cartaoCredito);
        return Created();
    }
}
