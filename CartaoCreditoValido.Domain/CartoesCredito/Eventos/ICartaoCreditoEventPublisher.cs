namespace CartaoCreditoValido.Domain.CartoesCredito.Eventos;

public interface ICartaoCreditoEventPublisher
{
    Task PublicarCartaoCriadoAsync(CartaoCreditoCriadoEvent evento, CancellationToken cancellationToken = default);
}

