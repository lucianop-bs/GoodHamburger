namespace GoodHamburger.Application.Utils.Responses
{
    public record class PedidoResponse(Guid Id,
        List<ItemPedidoResponse> Itens,
        decimal Subtotal,
        decimal Desconto,
        decimal TotalFinal);
}