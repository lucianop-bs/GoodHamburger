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

            
            builder.Property(p => p.Subtotal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.TotalFinal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Desconto).HasColumnType("decimal(18,2)");

            
            builder.HasMany(p => p.Itens)
                .WithOne()
                .HasForeignKey(i => i.PedidoId) 
                .IsRequired() 
                .OnDelete(DeleteBehavior.Cascade); 

            
            builder.Navigation(p => p.Itens)
                .HasField("_itens")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}