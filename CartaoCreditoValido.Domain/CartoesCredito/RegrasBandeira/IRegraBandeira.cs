namespace CartaoCreditoValido.Domain.CartoesCredito.RegrasBandeira;

public interface IRegraBandeira
{
    bool Suporta(long numeroCartao);

    bool Valido(long numeroCartao);
}