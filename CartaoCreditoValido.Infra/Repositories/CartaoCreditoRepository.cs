using CartaoCreditoValido.Domain.CartoesCredito.Entidades;
using CartaoCreditoValido.Domain.CartoesCredito.Repositorios;
using CartaoCreditoValido.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace CartaoCreditoValido.Infra.Repositories
{
    public class CartaoCreditoRepository : ICartaoCreditoRepository
    {
        private readonly CartaoCreditoContext _context;

        public CartaoCreditoRepository(CartaoCreditoContext context)
        {
            _context = context;
        }

        public async Task<CartaoCredito?> ObterPorId(long id, CancellationToken cancellationToken = default)
        {
            return await _context.CartoesCredito
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
   
        public async Task<CartaoCredito> ArmazenarAsync(CartaoCredito cartaoCredito, CancellationToken cancellationToken = default)
        {
            await _context.CartoesCredito.AddAsync(cartaoCredito, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
            
            return cartaoCredito;
        }
    }
}

