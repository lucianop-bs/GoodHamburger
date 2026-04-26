using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Utils.Mappers
{
    public static class PedidoMapper
    {
        public static List<PedidoResponse> ToPedidosResponse(this List<Pedido> pedidos)
        {
            return pedidos.Select(pedido => new PedidoResponse(
                pedido.Id,
                pedido.Itens.Select(item => item.ToItemPedidoResponse()).ToList(),
                pedido.Subtotal,
                pedido.Desconto,
                pedido.TotalFinal)
            ).ToList();
        }

        public static ItemPedidoResponse ToItemPedidoResponse(this ItemPedido item)
        {
            return new ItemPedidoResponse(
                item.ProdutoId,
                item.Produto.Nome,
                item.Categoria.ToString(),
                item.PrecoUnitario);
        }

        public static PedidoResponse ToPedidoResponse(this Pedido pedido)
        {
            return new PedidoResponse(
                pedido.Id,
                pedido.Itens.Select(item => item.ToItemPedidoResponse()).ToList(),
                pedido.Subtotal,
                pedido.Desconto,
                pedido.TotalFinal);
        }

        public static CriarPedidoResponse ToCriarPedidoResponse(this Pedido pedido)
        {
            return new CriarPedidoResponse(pedido.Id);
        }
    }
}