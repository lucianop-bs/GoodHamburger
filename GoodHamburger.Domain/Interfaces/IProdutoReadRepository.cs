using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IProdutoReadRepository
    {
        Task<List<Produto>> ObterProdutosAsync();

        Task<List<Produto>> ObterProdutosPorIdsAsync(List<int> ids);
    }
}