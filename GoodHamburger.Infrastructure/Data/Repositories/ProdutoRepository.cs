using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GoodHamburger.Infrastructure.Data.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly GoodHamburgerContext _context;

        public ProdutoRepository(GoodHamburgerContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> ObterTodosAsync()
        {

            return await _context.Produtos.AsNoTracking().ToListAsync();

        }
    }
}
