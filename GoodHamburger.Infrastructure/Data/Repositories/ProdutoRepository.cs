using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : IProdutoReadRepository
    {
        private readonly GoodHamburgerContext _context;

        public ProdutoRepository(GoodHamburgerContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterProdutosPorIdsAsync(List<int> ids)
        {
            return await _context.Produtos.AsNoTracking().Where(p => ids.Contains(p.Id)).ToListAsync();
        }

        public async Task<List<Produto>> ObterProdutosAsync()
        {
            return await _context.Produtos.AsNoTracking().ToListAsync();
        }
    }
}