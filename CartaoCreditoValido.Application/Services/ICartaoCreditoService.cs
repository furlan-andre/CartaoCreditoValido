
using CartaoCreditoValido.Domain.CartoesCredito.Entidades;

namespace CartaoCreditoValido.Application.Services
{
    public interface ICartaoCreditoService
    {
        Task Armazenar(CartaoCredito cartaoCredito, CancellationToken cancellationToken);
        Task<CartaoCredito> ObterCartaoCredito(long id, CancellationToken cancellationToken);
    }
}

