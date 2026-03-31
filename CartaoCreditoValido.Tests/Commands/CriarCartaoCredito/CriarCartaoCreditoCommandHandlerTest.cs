using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Services;
using CartaoCreditoValido.Domain.CartaoCredito.Validadores;
using CartaoCreditoValido.Domain.CartoesCredito.Eventos;
using CartaoCreditoValido.Domain.CartoesCredito.Entidades;
using FluentAssertions;
using Moq;

namespace CartaoCreditoValido.Tests.Commands.CriarCartaoCredito;

public class CriarCartaoCreditoCommandHandlerTest
{
    private readonly Mock<IValidadorNumeroCartao> _validadorNumeroCartaoMock;
    private readonly Mock<ICartaoCreditoService> _serviceMock;
    private readonly Mock<ICartaoCreditoEventPublisher> _eventPublisherMock;
    private readonly CriarCartaoCreditoCommandHandler _handler;

    public CriarCartaoCreditoCommandHandlerTest()
    {
        _validadorNumeroCartaoMock = new Mock<IValidadorNumeroCartao>();
        _serviceMock = new Mock<ICartaoCreditoService>();
        _eventPublisherMock = new Mock<ICartaoCreditoEventPublisher>();

        _serviceMock
            .Setup(s => s.Armazenar(It.IsAny<CartaoCredito>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((CartaoCredito cartao, CancellationToken _) =>
                new CriarCartaoCreditoDto(
                    cartao.Id,
                    cartao.NomeCompletoTitular,
                    cartao.NascimentoTitular,
                    cartao.NumeroCartao));

        _handler = new CriarCartaoCreditoCommandHandler(
            _validadorNumeroCartaoMock.Object,
            _serviceMock.Object,
            _eventPublisherMock.Object);
    }

    [Fact]
    public async Task DeveSalvarCartaoCreditoQuandoValidadoComSucesso()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand(
            "João Silva",
            new DateOnly(1990, 1, 1),
            4111111111111111);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        _validadorNumeroCartaoMock.Verify(
            v => v.Validar(command.NumeroCartao),
            Times.Once);

        _serviceMock.Verify(
            s => s.Armazenar(It.IsAny<CartaoCredito>(), It.IsAny<CancellationToken>()),
            Times.Once);

        _eventPublisherMock.Verify(
            x => x.PublicarCartaoCriadoAsync(It.IsAny<CartaoCreditoCriadoEvent>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task NaoDeveSalvarCartaoCreditoQuandoValidacaoFalhar()
    {
        // Arrange
        var command = new CriarCartaoCreditoCommand(
            "João Silva",
            new DateOnly(1990, 1, 1),
            4111111111111112);
    
        _validadorNumeroCartaoMock
            .Setup(v => v.Validar(command.NumeroCartao))
            .Throws(new Exception("Número do cartão inválido."));
    
        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);
    
        // Assert
        await act.Should()
            .ThrowAsync<Exception>()
            .WithMessage("Número do cartão inválido.");
    
        _serviceMock.Verify(
            s => s.Armazenar(It.IsAny<CartaoCredito>(), It.IsAny<CancellationToken>()),
            Times.Never);

        _eventPublisherMock.Verify(
            x => x.PublicarCartaoCriadoAsync(It.IsAny<CartaoCreditoCriadoEvent>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}