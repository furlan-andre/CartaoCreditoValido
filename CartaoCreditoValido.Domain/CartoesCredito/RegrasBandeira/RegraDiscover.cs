namespace CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

public class RegraDiscover : IRegraBandeira
{
    public bool Suporta(long numeroCartao)
    {
        var prefixo = numeroCartao.ToString()[..4];

        return prefixo.StartsWith("6011");
    }

    public bool Valido(long numeroCartao)
    {
        var quantidadeDigitos = numeroCartao.ToString().Length;

        if (quantidadeDigitos == 16)
            return true;

        return false;
    }
}