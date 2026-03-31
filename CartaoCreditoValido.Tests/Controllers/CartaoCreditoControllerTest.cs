using System.Net;
using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;
using CartaoCreditoValido.WebAPI.Controllers;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CartaoCreditoValido.Tests.Controllers;

public class CartaoCreditoControllerTest
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CartaoCreditoController _controller;
    
    public CartaoCreditoControllerTest()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new CartaoCreditoController(_mediatorMock.Object);
    }

    [Fact]
    public async Task DeveRetornarObjetoComSucesso()
    {
        var cartao = new ObterCartaoCreditoDto(
            1, "João Silva", new DateOnly(1990, 1, 1), 4111111111111111);
      
        _mediatorMock
            .Setup(x => x.Send(It.IsAny<ObterCartaoCreditoPorIdQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cartao);
        
        var resultado = await _controller.ObterPorId((int)cartao.Id, CancellationToken.None);

        var okResult = resultado.Should().BeOfType<OkObjectResult>().Subject;
        okResult.StatusCode.Should().Be((int) HttpStatusCode.OK);

        var cartaoCredito = okResult.Value.Should().BeAssignableTo<ObterCartaoCreditoDto>().Subject;
        cartaoCredito.Should().BeEquivalentTo(cartao);
    }
    
    [Fact]
    public async Task DeveRetornarExceptionQuandoNaoEncontrar()
    {
        var resultado = await _controller.ObterPorId(1, CancellationToken.None);

        var notFoundResult = resultado.Should().BeOfType<NotFoundResult>().Subject;

        Assert.IsType<NotFoundResult>(notFoundResult);
    }
    
    [Fact]
    public async Task DeveSalvarCartaoCreditoComSucesso()
    {
        var cartao = new CriarCartaoCreditoDto(
            1, "João Silva", new DateOnly(1990, 1, 1), 4111111111111111);

        var request = new CriarCartaoCreditoRequest()
        {
            NascimentoTitular = new DateOnly(1990, 1, 1),
            NumeroCartao = 4111111111111111,
            NomeCompletoTitular = "João Silva"
        };
        
        _mediatorMock
            .Setup(x => x.Send(It.IsAny<CriarCartaoCreditoCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(cartao);
        
        var resultado = await _controller.Criar(request, CancellationToken.None);

        var createdResult = resultado.Should().BeOfType<CreatedAtActionResult>().Subject;
        createdResult.StatusCode.Should().Be((int) HttpStatusCode.Created);

        var cartaoCredito = createdResult.Value.Should().BeAssignableTo<CriarCartaoCreditoDto>().Subject;
        cartaoCredito.Should().BeEquivalentTo(cartao);
    }
    
    [Fact]
    public async Task DeveRetornarBadRequestQuandoAlgoFalhar()
    {
        var resultado = await _controller.Criar(new CriarCartaoCreditoRequest(), CancellationToken.None);

        var createdResult = resultado.Should().BeOfType<BadRequestResult>().Subject;
       
        Assert.IsType<BadRequestResult>(createdResult);
    }
}