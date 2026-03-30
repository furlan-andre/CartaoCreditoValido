using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

namespace CartaoCreditoValido.Tests.RegrasBandeiras;

public class RegraAmexTest
{
    [Theory]
    [InlineData(345678912345678)]
    [InlineData(375678912345678)]
    public void DeveCriarCartaoCreditoAmexComSucesso(long numero)
    {
        // Amex válido = 15 dígitos e prefixo deve ser 34 ou 37
        var regra = new RegraAmex();

        var suporta = regra.Suporta(numero);
        var numeroValido = regra.Valido(numero);

        Assert.True(suporta);
        Assert.True(numeroValido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoAmexComPrefixoIncorreto()
    {
        // Amex válido = 15 dígitos e prefixo deve ser 34 ou 37
        var numero = 123456789012345;
        var regra = new RegraAmex();

        var suporta = regra.Suporta(numero);

        Assert.False(suporta);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoAmexComTamanhoMaior()
    {
        // Amex válido = 15 dígitos e prefixo deve ser 34 ou 37
        var numero = 3444123456789012345;
        var regra = new RegraAmex();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoAmexComTamanhoMenor()
    {
        // Amex válido = 15 dígitos e prefixo deve ser 34 ou 37
        var numero = 34123;
        var regra = new RegraAmex();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }
}