
using CartaoCreditoValido.Domain.CartoesCredito.Entities;

namespace CartaoCreditoValido.Application.Services
{
    public interface ICartaoCreditoService
    {
        Task Armazenar(CartaoCredito cartaoCredito, CancellationToken cancellationToken);
    }
}

