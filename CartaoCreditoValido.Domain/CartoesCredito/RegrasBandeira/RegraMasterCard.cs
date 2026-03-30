namespace CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

public class RegraMasterCard : IRegraBandeira
{
    public bool Suporta(long numeroCartao)
    {
        var prefixo = numeroCartao.ToString()[..2];
        return prefixo is "51" or "52" or "53" or "54" or "55";
    }

    public bool Valido(long numeroCartao)
    {
        var quantidadeDigitos = numeroCartao.ToString().Length;

        if (quantidadeDigitos == 16)
            return true;

        return false;
    }
}