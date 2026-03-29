using Microsoft.AspNetCore.Mvc;

namespace CartaoCreditoValido.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ValidoController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok();
    }
}
