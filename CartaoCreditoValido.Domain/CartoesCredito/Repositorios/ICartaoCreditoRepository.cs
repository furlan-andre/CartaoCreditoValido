namespace CartaoCreditoValido.Domain.CartoesCredito.Repositorios;

public interface ICartaoCreditoRepository
{
    Task<Entidades.CartaoCredito> ObterPorId(long id);
    Task<Entidades.CartaoCredito> ArmazenarAsync(Entidades.CartaoCredito cartaoCredito);
}
