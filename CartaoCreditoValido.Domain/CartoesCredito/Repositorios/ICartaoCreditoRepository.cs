namespace CartaoCreditoValido.Domain.CartoesCredito.Repositorios;

public interface ICartaoCreditoRepository
{
    Task<Entidades.CartaoCredito?> ObterPorId(long id, CancellationToken cancellationToken);
    Task<Entidades.CartaoCredito> ArmazenarAsync(Entidades.CartaoCredito cartaoCredito, CancellationToken cancellationToken);
}
