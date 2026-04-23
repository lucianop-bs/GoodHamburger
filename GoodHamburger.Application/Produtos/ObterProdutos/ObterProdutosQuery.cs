using GoodHamburger.Application.Utils.Responses;
using MediatR;

namespace GoodHamburger.Application.Produtos.ObterProdutos
{
    public record class ObterProdutosQuery : IRequest<List<ProdutoResponse>> { }
}