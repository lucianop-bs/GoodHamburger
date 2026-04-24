namespace GoodHamburger.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        private readonly List<ItemPedido> _itens;
        public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();
        public decimal Subtotal { get; private set; }
        public decimal Desconto { get; private set; }
        public decimal TotalFinal { get; private set; }

        public Pedido()
        {
            Id = Guid.NewGuid();
            _itens = new List<ItemPedido>();
            Subtotal = 0;
            Desconto = 0;
            TotalFinal = 0;
        }

        public void AdicionarItem(Produto produto)
        {
            var item = new ItemPedido(produto.Id, produto.Categoria, produto.Preco);

            _itens.Add(item);

            CalcularSubtotal();
        }

        public void CalcularSubtotal()
        {
            Subtotal = _itens.Sum(i => i.PrecoUnitario);
            CalcularTotalFinal();
        }

        public void CalcularTotalFinal()
        {
            decimal desconto = Subtotal * (Desconto / 100m);
            TotalFinal = Subtotal - desconto;
        }

        public void AplicarDesconto(decimal desconto)
        {
            Desconto = desconto;
            CalcularTotalFinal();
        }
    }
}