using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class PedidoRepository : IPedidoReadRepository, IPedidoWriteRepository
    {
        private readonly GoodHamburgerContext _context;

        public PedidoRepository(GoodHamburgerContext context)
        {
            _context = context;
        }

        public async Task AdicionarPedidoAsync(Pedido pedido)
        {
            await _context.Pedidos.AddAsync(pedido);
        }

        public async Task<List<Pedido>> ObterPedidosAsync()
        {
            return await _context.Pedidos.Include(p => p.Itens).AsNoTracking().ToListAsync();
        }
    }
}