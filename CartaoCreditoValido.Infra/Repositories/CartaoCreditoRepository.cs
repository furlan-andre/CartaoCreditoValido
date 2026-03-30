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

        public async Task<CartaoCredito> ObterPorId(long id)
        {
            return await _context.CartoesCredito.Where(x => x.Id == id).FirstOrDefaultAsync();
        }
   
        public async Task<CartaoCredito> ArmazenarAsync(CartaoCredito cartaoCredito)
        {
            await _context.CartoesCredito.AddAsync(cartaoCredito);
            await _context.SaveChangesAsync();
            
            return cartaoCredito;
        }
    }
}

