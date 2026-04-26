using GoodHamburger.Domain.Enums;

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
            AplicarDescontoAuto();
        }

        private void CalcularSubtotal()
        {
            Subtotal = _itens.Sum(i => i.PrecoUnitario);
            CalcularTotalFinal();
        }

        private void CalcularTotalFinal()
        {
            decimal desconto = Subtotal * (Desconto / 100m);
            TotalFinal = Subtotal - desconto;
        }

        public void AplicarDesconto(decimal desconto)
        {
            Desconto = desconto;
            CalcularTotalFinal();
        }

        public void AtualizarPedido(List<Produto> produtos)
        {
            _itens.Clear();

            foreach (var produto in produtos)
            {
                var novoItem = new ItemPedido(produto.Id, produto.Categoria, produto.Preco);
                _itens.Add(novoItem);
            }

            CalcularSubtotal();
            AplicarDescontoAuto();
        }

        public void AplicarDescontoAuto()
        {
            var temSanduiche = _itens.Any(i => i.Categoria == Categoria.Sanduiche);

            var temBebida = _itens.Any(i => i.Categoria == Categoria.Bebida);

            var temAcompanhamento = _itens.Any(i => i.Categoria == Categoria.Acompanhamento);

            if (temSanduiche && temBebida && temAcompanhamento)
            {
                AplicarDesconto(20);
            }
            else if (temSanduiche && temBebida)
            {
                AplicarDesconto(15);
            }
            else if (temAcompanhamento && temSanduiche)
            {
                AplicarDesconto(10);
            }
            else
                AplicarDesconto(0);
        }
    }
}