
using CartaoCreditoValido.Application.Commands.CriarCartaoCredito;
using CartaoCreditoValido.Application.Queries.ObterCartaoCreditoPorId;
using CartaoCreditoValido.Domain.CartoesCredito.Entidades;

namespace CartaoCreditoValido.Application.Services
{
    public interface ICartaoCreditoService
    {
        Task<CriarCartaoCreditoDto?> Armazenar(CartaoCredito cartaoCredito, CancellationToken cancellationToken);
        Task<ObterCartaoCreditoDto?> ObterCartaoCredito(long id, CancellationToken cancellationToken);
    }
}

