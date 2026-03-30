namespace CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

public class RegraAmex : IRegraBandeira
{
    public bool Suporta(long numeroCartao)
    {
        var numero = numeroCartao.ToString();

        return numero.StartsWith("34") || numero.StartsWith("37");
    }

    public bool Valido(long numeroCartao)
    {
        var quantidadeDigitos = numeroCartao.ToString().Length;

        if (quantidadeDigitos == 15)
            return true;

        return false;
    }
}