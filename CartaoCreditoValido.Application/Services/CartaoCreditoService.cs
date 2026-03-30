using CartaoCreditoValido.Domain.CartoesCredito.Entities;

namespace CartaoCreditoValido.Application.Services
{
    public class CartaoCreditoService : ICartaoCreditoService
    {
        public Task Armazenar(CartaoCredito cartaoCredito, CancellationToken cancellationToken)
        {
            // TODO: Implementar repositorio
            cartaoCredito.Id = 1;
            return default;
        }
    }
}
