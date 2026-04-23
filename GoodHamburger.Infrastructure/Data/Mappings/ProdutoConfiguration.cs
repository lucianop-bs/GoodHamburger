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
            builder.Property(p => p.Id)
                   .ValueGeneratedOnAdd();
            builder.Property(p => p.Nome)
                 .IsRequired()
                 .HasMaxLength(100);
            builder.Property(p => p.Categoria)
                 .IsRequired()
                 .HasConversion<string>();
            builder.Property(p => p.Preco)
                 .HasColumnType("decimal(10,2)");

            builder.HasData(
                new Produto(1, "X Burger", Domain.Enums.Categoria.Sanduiche, 5.00m),
                new Produto(2, "X Egg", Domain.Enums.Categoria.Sanduiche, 4.50m),
                new Produto(3, "X Bacon", Domain.Enums.Categoria.Sanduiche, 7.00m),
                new Produto(4, "Batata Frita", Domain.Enums.Categoria.Acompanhamento, 2.00m),
                new Produto(5, "Refrigerante", Domain.Enums.Categoria.Bebida, 2.50m)
            );
        }
    }
}