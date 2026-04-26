using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Mappings
{
    public class ItemPedidoConfiguration : IEntityTypeConfiguration<ItemPedido>
    {
        public void Configure(EntityTypeBuilder<ItemPedido> builder)
        {
            builder.ToTable("ItensPedido");
            builder.HasKey(i => i.Id);

            builder.Property(i => i.PrecoUnitario)
                   .HasColumnType("decimal(10,2)");

            builder.Property(p => p.Categoria)
                .IsRequired()
                .HasConversion<string>();

            builder.HasOne(i => i.Produto)
                .WithMany()
                .HasForeignKey(i => i.ProdutoId)
                .IsRequired();
        }
    }
}