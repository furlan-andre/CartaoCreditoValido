using CartaoCreditoValido.Domain.CartoesCredito.Entidades;
using CartaoCreditoValido.Domain.CartoesCredito.Repositorios;

namespace CartaoCreditoValido.Application.Services
{
    public class CartaoCreditoService : ICartaoCreditoService
    {
        private readonly ICartaoCreditoRepository _cartaoCreditoRepository;

        public CartaoCreditoService(ICartaoCreditoRepository cartaoCreditoRepository)
        {
            _cartaoCreditoRepository = cartaoCreditoRepository;
        }

        public async Task Armazenar(CartaoCredito cartaoCredito, CancellationToken cancellationToken = default)
        {
            await _cartaoCreditoRepository.ArmazenarAsync(cartaoCredito);
        }

        public async Task<CartaoCredito> ObterCartaoCredito(long id, CancellationToken cancellationToken = default)
        {
            return await _cartaoCreditoRepository.ObterPorId(id);
        }
    }
}
