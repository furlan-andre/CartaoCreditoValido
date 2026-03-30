using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

namespace CartaoCreditoValido.Tests.RegrasBandeiras;

public class RegraVisaTest
{
    [Theory]
    [InlineData(4567891234567)]
    [InlineData(4567891234567890)]
    public void DeveCriarCartaoCreditoVisaComSucesso(long numero)
    {
        // Visa válido = 13 ou 16 dígitos e prefixo deve ser 4
        var regra = new RegraVisa();

        var suporta = regra.Suporta(numero);
        var numeroValido = regra.Valido(numero);

        Assert.True(suporta);
        Assert.True(numeroValido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoVisaComPrefixoIncorreto()
    {
        // Visa válido = 13 ou 16 dígitos e prefixo deve ser 4
        var numero = 123456789012345;
        var regra = new RegraVisa();

        var suporta = regra.Suporta(numero);

        Assert.False(suporta);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoVisaComTamanhoMaior()
    {
        // Visa válido = 13 ou 16 dígitos e prefixo deve ser 4
        var numero = 4444123456789012345;
        var regra = new RegraVisa();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoVisaComTamanhoMenor()
    {
        // Visa válido = 13 ou 16 dígitos e prefixo deve ser 4
        var numero = 44123;
        var regra = new RegraVisa();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }
}