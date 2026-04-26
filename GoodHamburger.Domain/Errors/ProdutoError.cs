using GoodHamburger.Domain.Results;

namespace GoodHamburger.Domain.Errors
{
    public static class ProdutoError
    {
        public static Error ProdutoNaoEncontrado => new Error("Produto não encontrado", ErrorType.NotFound);
    }
}