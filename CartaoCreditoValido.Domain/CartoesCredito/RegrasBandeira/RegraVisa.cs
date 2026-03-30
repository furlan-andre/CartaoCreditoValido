namespace CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

public class RegraVisa : IRegraBandeira
{
    public bool Suporta(long numeroCartao)
    {
        var numero = numeroCartao.ToString();

        return numero.StartsWith("4");
    }

    public bool Valido(long numeroCartao)
    {
        var quantidadeDigitos = numeroCartao.ToString().Length;

        if (quantidadeDigitos == 13 || quantidadeDigitos == 16)
            return true;

        return false;
    }
}