using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;
using CartaoCreditoValido.Domain.CartoesCredito.Services.Validadores;
using CartaoCreditoValido.Domain.Commons.Exceptions;
using FluentAssertions;
using Moq;

namespace CartaoCreditoValido.Tests.Validadores;

public class ValidadorNumeroCartaoTest
{
    [Theory]
    [InlineData(4111111111111111)]
    [InlineData(4012888888881881)]
    [InlineData(378282246310005)]
    [InlineData(6011111111111117)]
    [InlineData(5105105105105100)]
    public void DeveCriarCartaoCreditoUsandoNumeroCartaoValidadoPreviamente(long numeroCartao)
    {
        // Arrange
       var regras = new List<IRegraBandeira>()
        {
            new RegraAmex(),
            new RegraDiscover(),
            new RegraVisa(),
            new RegraMasterCard()
        };
        
        var sut = new ValidadorNumeroCartao(regras);

        // Act
        Action act = () => sut.Validar(numeroCartao);

        // Assert
        act.Should().NotThrow();
    }
    
    [Theory]
    [InlineData(4111111111111)]
    [InlineData(5105105105105106)]
    [InlineData(9111111111111111)]
    public void DeveCriarCartaoCreditoUsandoNumeroCartaoInvalidadoPreviamente(long numeroCartao)
    {
        // Arrange
        var regras = new List<IRegraBandeira>()
        {
            new RegraAmex(),
            new RegraDiscover(),
            new RegraVisa(),
            new RegraMasterCard()
        };
        
        var sut = new ValidadorNumeroCartao(regras);

        // Act
        Action act = () => sut.Validar(numeroCartao);

        // Assert
        act.Should()
            .Throw<DomainException>();
    }
    
    [Fact]
    public void NaoDeveCriarParaCartaoComBandeiraDesconhecida()
    {
        // Arrange
        const long numeroCartao = 9999999999999999;
        
        var regra = CriarRegra(suporta: false, valido: true);
        
        var sut = new ValidadorNumeroCartao(new[] { regra.Object });

        // Act
        Action act = () => sut.Validar(numeroCartao);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Cartão desconhecido.");
    }

    [Fact]
    public void NaoDeveCriarParaCartaoComQuantidadeDigitosIncorreta()
    {
        // Arrange
        const long numeroCartao = 4111111111111111;

        var regra = CriarRegra(suporta: true, valido: false);

        var sut = new ValidadorNumeroCartao(new[] { regra.Object });

        // Act
        Action act = () => sut.Validar(numeroCartao);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Quantidade de dígitos incorreta.");
    }

    [Fact]
    public void NaoDeveCriarParaCartaoComLuhnInvalido()
    {
        // Arrange
        const long numeroCartao = 4111111111111112;

        var regra = CriarRegra(suporta: true, valido: true);

        var sut = new ValidadorNumeroCartao(new[] { regra.Object });

        // Act
        Action act = () => sut.Validar(numeroCartao);

        // Assert
        act.Should()
            .Throw<DomainException>()
            .WithMessage("Cartão inválido.");
    }

    [Fact]
    public void DeveCriarCartaoCreditoQuandoNumeroValido()
    {
        // Arrange
        const long numeroCartao = 4111111111111111;

        var regra = CriarRegra(suporta: true, valido: true);

        var sut = new ValidadorNumeroCartao(new[] { regra.Object });

        // Act
        Action act = () => sut.Validar(numeroCartao);

        // Assert
        act.Should().NotThrow();
    }

    [Fact]
    public void DeveSelecionarRegraCorretaParaRealizarValidacao()
    {
        // Arrange
        const long numeroCartao = 4111111111111111;

        var primeiraRegra = new Mock<IRegraBandeira>();
        primeiraRegra.Setup(r => r.Suporta(numeroCartao)).Returns(true);
        primeiraRegra.Setup(r => r.Valido(numeroCartao)).Returns(true);

        var segundaRegra = new Mock<IRegraBandeira>();
        segundaRegra.Setup(r => r.Suporta(numeroCartao)).Returns(false);

        var sut = new ValidadorNumeroCartao(new[] { primeiraRegra.Object, segundaRegra.Object });

        // Act
        sut.Validar(numeroCartao);

        // Assert
        primeiraRegra.Verify(r => r.Suporta(numeroCartao), Times.Once);
        primeiraRegra.Verify(r => r.Valido(numeroCartao), Times.Once);

        segundaRegra.Verify(r => r.Suporta(numeroCartao), Times.Never);
        segundaRegra.Verify(r => r.Valido(It.IsAny<long>()), Times.Never);
    }

    private static Mock<IRegraBandeira> CriarRegra(bool suporta, bool valido)
    {
        var mock = new Mock<IRegraBandeira>();

        mock.Setup(r => r.Suporta(It.IsAny<long>()))
            .Returns(suporta);

        mock.Setup(r => r.Valido(It.IsAny<long>()))
            .Returns(valido);

        return mock;
    }
}