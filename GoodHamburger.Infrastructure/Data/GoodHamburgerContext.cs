using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GoodHamburger.Infrastructure.Data
{
    public class GoodHamburgerContext(DbContextOptions<GoodHamburgerContext> options) : DbContext(options)
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}