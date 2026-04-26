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
            builder.Property(p => p.Id).ValueGeneratedNever();
            builder.Property(p => p.Subtotal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.TotalFinal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Desconto).HasColumnType("decimal(18,2)");

            builder.OwnsMany(p => p.Itens, item =>  
            {
                item.ToTable("ItensPedido");
                item.WithOwner().HasForeignKey(i => i.PedidoId);
                item.HasKey(i => i.Id);
                item.Property(i => i.Id).ValueGeneratedNever();
                item.Property(i => i.PrecoUnitario).HasColumnType("decimal(10,2)");
                item.Property(i => i.Categoria).IsRequired().HasConversion<string>();

                item.HasOne(i => i.Produto)
                    .WithMany()
                    .HasForeignKey(i => i.ProdutoId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            builder.Navigation(p => p.Itens)
                   .HasField("_itens")
                   .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}