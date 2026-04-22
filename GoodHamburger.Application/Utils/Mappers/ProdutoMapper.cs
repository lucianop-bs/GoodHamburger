using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Application.Utils.Mappers
{
    public static class ProdutoMapper
    {
        public static ProdutoResponse ToProdutoResponse(this Produto produto)
        {
            return new ProdutoResponse(produto.Id, produto.Nome, produto.Categoria.ToString(), produto.Preco);
          
        }
        public static List<ProdutoResponse> ToListaProdutosResponse(this List<Produto> produtos)
        {
            return produtos.Select(p => p.ToProdutoResponse()).ToList();

        }
    }
}
