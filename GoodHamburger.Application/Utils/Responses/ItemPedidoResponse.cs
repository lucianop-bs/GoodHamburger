namespace GoodHamburger.Application.Utils.Responses
{
    public record ItemPedidoResponse(int ProdutoId, string NomePedido, string Categoria, decimal PrecoUnitario);
}