using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Domain.Entities
{
    public class ItemPedido
    {
        public Guid Id { get; private set; }
        public Guid ProdutoId { get; private set; }
        public Categoria Categoria { get; private set; }
        public decimal PrecoUnitario { get; private set; }

        public ItemPedido(Guid produtoId, Categoria categoria, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
            ProdutoId = produtoId;
            Categoria = categoria;
            PrecoUnitario = precoUnitario;
        }
    }
}