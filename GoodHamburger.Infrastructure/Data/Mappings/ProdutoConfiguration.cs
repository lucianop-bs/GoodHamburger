using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Mappings
{
    public class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.ToTable("Produtos");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome)
                 .IsRequired()
                 .HasMaxLength(100);
            builder.Property(p => p.Categoria)
                 .IsRequired()
                 .HasConversion<string>();
            builder.Property(p => p.Preco)
                 .HasColumnType("decimal(10,2)");
        }
    }
}