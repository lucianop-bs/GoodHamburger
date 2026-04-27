using FluentAssertions;
using GoodHamburger.Domain.Entities;
using GoodHamburger.Domain.Enums;

namespace GoodHamburger.Tests.Domain
{
    public class PedidoTests
    {
        private static Produto CriarSanduiche() =>
            new Produto(1, "X Burger", Categoria.Sanduiche, 5.00m);

        private static Produto CriarBebida() =>
            new Produto(2, "Refrigerante", Categoria.Bebida, 2.50m);

        private static Produto CriarAcompanhamento() =>
            new Produto(3, "Batata Frita", Categoria.Acompanhamento, 2.00m);

        [Fact]
        public void AdicionarItem_SemCombo_NaoDeveAplicarDesconto()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            pedido.AdicionarItem(CriarSanduiche());

            // Assert
            pedido.Desconto.Should().Be(0);
            pedido.TotalFinal.Should().Be(5.00m);
        }

        [Fact]
        public void AdicionarItem_SanduicheEBebida_DeveAplicar15PorCento()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            pedido.AdicionarItem(CriarSanduiche());
            pedido.AdicionarItem(CriarBebida());

            // Assert
            pedido.Subtotal.Should().Be(7.50m);
            pedido.Desconto.Should().Be(15);
            pedido.TotalFinal.Should().Be(6.375m);
        }

        [Fact]
        public void AdicionarItem_SanduicheEAcompanhamento_DeveAplicar10PorCento()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            pedido.AdicionarItem(CriarSanduiche());
            pedido.AdicionarItem(CriarAcompanhamento());

            // Assert
            pedido.Desconto.Should().Be(10);
            pedido.TotalFinal.Should().Be(6.30m);
        }

        [Fact]
        public void AdicionarItem_ComboCompleto_DeveAplicar20PorCento()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            pedido.AdicionarItem(CriarSanduiche());
            pedido.AdicionarItem(CriarBebida());
            pedido.AdicionarItem(CriarAcompanhamento());

            // Assert
            pedido.Subtotal.Should().Be(9.50m);
            pedido.Desconto.Should().Be(20);
            pedido.TotalFinal.Should().Be(7.60m);
        }

        [Fact]
        public void AdicionarItem_BebidaEAcompanhamento_NaoDeveAplicarDesconto()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            pedido.AdicionarItem(CriarBebida());
            pedido.AdicionarItem(CriarAcompanhamento());

            // Assert
            pedido.Desconto.Should().Be(0);
            pedido.TotalFinal.Should().Be(4.50m);
        }

        [Fact]
        public void AdicionarItem_ProdutoNulo_DeveLancarExcecao()
        {
            // Arrange
            var pedido = new Pedido();

            // Act
            Action action = () => pedido.AdicionarItem(null);

            // Assert
            action.Should().Throw<NullReferenceException>();
        }

        [Fact]
        public void AtualizarPedido_SemItens_DeveZerarValores()
        {
            // Arrange
            var pedido = new Pedido();
            pedido.AdicionarItem(CriarSanduiche());
            pedido.AdicionarItem(CriarBebida());

            // Act
            pedido.AtualizarPedido(new List<Produto>());

            // Assert
            pedido.Itens.Should().BeEmpty();
            pedido.Subtotal.Should().Be(0);
            pedido.Desconto.Should().Be(0);
            pedido.TotalFinal.Should().Be(0);
        }
    }
}