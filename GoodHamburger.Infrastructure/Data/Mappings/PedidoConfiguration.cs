using GoodHamburger.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GoodHamburger.Infrastructure.Data.Mappings
{
    public class PedidoConfiguration : IEntityTypeConfiguration<Pedido>
    {
        public void Configure(EntityTypeBuilder<Pedido> builder)
        {
            builder.ToTable("Pedidos");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Subtotal)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TotalFinal)
               .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Desconto)
               .HasColumnType("decimal(18,2)");

            builder.Metadata.FindNavigation(nameof(Pedido.Itens))
            ?.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}