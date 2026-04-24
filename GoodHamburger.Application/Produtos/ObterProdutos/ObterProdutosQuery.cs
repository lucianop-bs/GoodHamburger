using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Produtos.ObterProdutos
{
    public record class ObterProdutosQuery : IRequest<Result<List<ProdutoResponse>>>;
}