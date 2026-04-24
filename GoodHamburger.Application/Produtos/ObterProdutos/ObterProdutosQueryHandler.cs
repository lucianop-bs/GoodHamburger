using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Interfaces;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Produtos.ObterProdutos
{
    public class ObterProdutosQueryHandler : IRequestHandler<ObterProdutosQuery, Result<List<ProdutoResponse>>>
    {
        private readonly IProdutoReadRepository _produtoRepository;

        public ObterProdutosQueryHandler(IProdutoReadRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<Result<List<ProdutoResponse>>> Handle(ObterProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.ObterProdutosAsync();

            var response = produtos.ToListaProdutosResponse();

            return Result<List<ProdutoResponse>>.Success(response);
        }
    }
}