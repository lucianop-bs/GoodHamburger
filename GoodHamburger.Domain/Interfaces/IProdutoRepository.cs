using GoodHamburger.Domain.Entities;

namespace GoodHamburger.Domain.Interfaces
{
    public interface IProdutoRepository
    {
        Task<List<Produto>> ObterTodosAsync();
    }
}
