namespace CartaoCreditoValido.Domain.CartaoCredito.Validadores;

public interface IValidadorNumeroCartao
{
    void Validar(long numeroCartao);
}