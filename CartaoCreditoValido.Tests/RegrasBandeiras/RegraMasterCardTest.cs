using CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

namespace CartaoCreditoValido.Tests.RegrasBandeiras;

public class RegraMasterCardTest
{
    [Theory]
    [InlineData(5111789123456789)]
    [InlineData(5211789123456789)]
    [InlineData(5311789123456789)]
    [InlineData(5411789123456789)]
    [InlineData(5511789123456789)]
    public void DeveCriarCartaoCreditoMasterCardComSucesso(long numero)
    {
        // MasterCard válido = 16 dígitos e prefixo deve ser 51 - 55
        var regra = new RegraMasterCard();

        var suporta = regra.Suporta(numero);
        var numeroValido = regra.Valido(numero);

        Assert.True(suporta);
        Assert.True(numeroValido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoMasterCardComPrefixoIncorreto()
    {
        // MasterCard válido = 16 dígitos e prefixo deve ser 51 - 55
        var numero = 5011789123456789;
        var regra = new RegraMasterCard();
        
        var suporta = regra.Suporta(numero);

        Assert.False(suporta);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoMasterCardComTamanhoMaior()
    {
        // MasterCard válido = 16 dígitos e prefixo deve ser 51 - 55
        var numero = 541178912345678912;
        var regra = new RegraMasterCard();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }

    [Fact]
    public void NaoDeveCriarCartaoCreditoMasterCardComTamanhoMenor()
    {
        // MasterCard válido = 16 dígitos e prefixo deve ser 51 - 55
        var numero = 5100;
        var regra = new RegraMasterCard();

        var tamanhoInvalido = regra.Valido(numero);

        Assert.False(tamanhoInvalido);
    }
}