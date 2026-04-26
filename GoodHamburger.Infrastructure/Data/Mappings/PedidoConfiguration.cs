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

            // Configuração de Decimais
            builder.Property(p => p.Subtotal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.TotalFinal).HasColumnType("decimal(18,2)");
            builder.Property(p => p.Desconto).HasColumnType("decimal(18,2)");

            // CONFIGURAÇÃO ÚNICA E CORRETA DO RELACIONAMENTO
            builder.HasMany(p => p.Itens)
                .WithOne()
                .HasForeignKey("PedidoId") // Define o nome da coluna no banco
                .IsRequired() // Um item não pode existir sem um pedido
                .OnDelete(DeleteBehavior.Cascade); // Se remover da lista ou deletar o pedido, apaga o item

            // Acesso ao campo privado para respeitar o Domínio Rico
            builder.Navigation(p => p.Itens)
                .HasField("_itens")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}