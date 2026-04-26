namespace GoodHamburger.Application.Utils.Responses
{
    public record class ItemPedidoResponse(int ProdutoId, string NomePedido, string Categoria, decimal PrecoUnitario);
}