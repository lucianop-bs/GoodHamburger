namespace GoodHamburger.Domain.Entities
{
    public class Pedido
    {
        public Guid Id { get; private set; }
        private readonly List<ItemPedido> _itens;
        public IReadOnlyCollection<ItemPedido> Itens => _itens.AsReadOnly();
        public decimal Subtotal { get; private set; }
        public double Desconto { get; private set; }
        public decimal TotalFinal { get; private set; }

        public Pedido()
        {
            Id = Guid.NewGuid();
            _itens = new List<ItemPedido>();
            Subtotal = 0;
            Desconto = 0;
            TotalFinal = 0;
        }

    }
}
