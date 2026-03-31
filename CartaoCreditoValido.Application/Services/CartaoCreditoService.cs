using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;
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

        public async Task<CriarCartaoCreditoDto?> Armazenar(CartaoCredito cartaoCredito, CancellationToken cancellationToken = default)
        {
            var cartaoCreditoArmazenado = await _cartaoCreditoRepository.ArmazenarAsync(cartaoCredito, cancellationToken);
            
            var resultado = new CriarCartaoCreditoDto(
                cartaoCreditoArmazenado.Id,
                cartaoCreditoArmazenado.NomeCompletoTitular,
                cartaoCreditoArmazenado.NascimentoTitular,
                cartaoCreditoArmazenado.NumeroCartao);
            
            return resultado;
        }

        public async Task<ObterCartaoCreditoDto?> ObterCartaoCredito(long id, CancellationToken cancellationToken = default)
        {
            var cartao = await _cartaoCreditoRepository.ObterPorId(id, cancellationToken);

            if (cartao is null)
                return null;
            
            return new ObterCartaoCreditoDto(
                cartao.Id,
                cartao.NomeCompletoTitular,
                cartao.NascimentoTitular,
                cartao.NumeroCartao);
        }
    }
}
