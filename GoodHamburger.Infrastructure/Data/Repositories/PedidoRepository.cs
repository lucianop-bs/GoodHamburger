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

        public void AtualizarPedido(Pedido pedido)
        {
            var itensAntigos = _context.Set<ItemPedido>()
                         .Where(i => i.PedidoId == pedido.Id)
                         .ToList();

  
            _context.Set<ItemPedido>().RemoveRange(itensAntigos);
        }

        public void DeletarPedido(Pedido pedido)
        {
            _context.Pedidos.Remove(pedido);
        }

        public async Task<Pedido?> ObterPedidoPorIdAsync(Guid id)
        {
            return await _context.Pedidos.Include(p => p.Itens).ThenInclude(i => i.Produto).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Pedido>> ObterPedidosAsync()
        {
            return await _context.Pedidos.Include(p => p.Itens).ThenInclude(i => i.Produto).AsNoTracking().ToListAsync();
        }
    }
}