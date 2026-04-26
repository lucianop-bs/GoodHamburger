namespace GoodHamburger.Application.Utils.Responses
{
    public record PedidoResponse(Guid Id,
        List<ItemPedidoResponse> Itens,
        decimal Subtotal,
        decimal Desconto,
        decimal TotalFinal);
}