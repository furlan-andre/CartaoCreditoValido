using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

namespace CartaoCreditoValido.Tests.RegrasBandeiras;

public class RegraDiscoverTest
{
    [Theory]
    [InlineData(6011789123456789)]
    public void DeveCriarCartaoCreditoDiscoverComSucesso(long numero)
    {
        // Discover válido = 16 dígitos e prefixo deve ser 6011
        var regra = new RegraDiscover();

        var suporta = regra.Suporta(numero);
        var numeroValido = regra.Valido(numero);

        Assert.True(suporta);
        Assert.True(numeroValido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoDiscoverComPrefixoIncorreto()
    {
        // Discover válido = 16 dígitos e prefixo deve ser 6011
        var numero = 6111789123456789;
        var regra = new RegraDiscover();

        var suporta = regra.Suporta(numero);

        Assert.False(suporta);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoDiscoverComTamanhoMaior()
    {
        // Discover válido = 16 dígitos e prefixo deve ser 6011
        var numero = 611178912345678912;
        var regra = new RegraDiscover();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoDiscoverComTamanhoMenor()
    {
        // Discover válido = 16 dígitos e prefixo deve ser 6011
        var numero = 601100;
        var regra = new RegraDiscover();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }
}