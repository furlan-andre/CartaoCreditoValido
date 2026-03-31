using CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;
using CartaoCreditoValido.Application.Services;
using Moq;

namespace CartaoCreditoValido.Tests.Queries.ObterCartaoCredito;

public sealed class ObterCartaoCreditoPorIdQueryHandlerTest
{
    private readonly Mock<ICartaoCreditoService> _cartaoCreditoServiceMock;
    private readonly ObterCartaoCreditoPorIdQueryHandler _handler;

    public ObterCartaoCreditoPorIdQueryHandlerTest()
    {
        _cartaoCreditoServiceMock = new Mock<ICartaoCreditoService>();
        _handler = new ObterCartaoCreditoPorIdQueryHandler(_cartaoCreditoServiceMock.Object);
    }

    [Fact]
    public async Task DeveRetornarObjetoQuandoEncontradoNoBanco()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var query = new ObterCartaoCreditoPorIdQuery(1);

        var dtoEsperado = new ObterCartaoCreditoDto(
            1, "Fulano de Tal", new DateOnly(1990, 1, 1), 4111111111111111);

        _cartaoCreditoServiceMock
            .Setup(x => x.ObterCartaoCredito(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(dtoEsperado);

        // Act
        var resultado = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.NotNull(resultado);
        Assert.Same(dtoEsperado, resultado);

        _cartaoCreditoServiceMock.Verify(
            x => x.ObterCartaoCredito(query.Id, cancellationToken),
            Times.Once);
    }

    [Fact]
    public async Task DeveRetornarNullQuandoNaoEncotrarObjetoNoBanco()
    {
        // Arrange
        var cancellationToken = CancellationToken.None;
        var query = new ObterCartaoCreditoPorIdQuery(99);

        _cartaoCreditoServiceMock
            .Setup(x => x.ObterCartaoCredito(It.IsAny<long>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((ObterCartaoCreditoDto?)null);

        // Act
        var resultado = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Null(resultado);

        _cartaoCreditoServiceMock.Verify(
            x => x.ObterCartaoCredito(query.Id, cancellationToken),
            Times.Once);
    }
}