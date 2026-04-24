using GoodHamburger.Application.Utils.Mappers;
using GoodHamburger.Application.Utils.Responses;
using GoodHamburger.Domain.Interfaces;
using MediatR;

namespace GoodHamburger.Application.Produtos.ObterProdutos
{
    public class ObterProdutosQueryHandler : IRequestHandler<ObterProdutosQuery, List<ProdutoResponse>>
    {
        private readonly IProdutoReadRepository _produtoRepository;

        public ObterProdutosQueryHandler(IProdutoReadRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<List<ProdutoResponse>> Handle(ObterProdutosQuery request, CancellationToken cancellationToken)
        {
            var produtos = await _produtoRepository.ObterProdutosAsync();

            return produtos.ToListaProdutosResponse();
        }
    }
}