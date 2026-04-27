using GoodHamburger.Domain.Results;

namespace GoodHamburger.Domain.Errors
{
    public static class ProdutoError
    {
        public static Error ProdutoNaoEncontrado =>
            new Error("Produto não encontrado.",
                ErrorType.NotFound);

        public static Error IdInvalido =>
            new Error("ID do produto é inválido.",
                ErrorType.Validation);

        public static Error ValoresNegativos =>
            new Error("IDs de produtos devem ser números positivos.",
                ErrorType.Validation);

        public static Error IdsInvalidosNoCardapio =>
            new Error("Produtos não existem no cardápio.",
                ErrorType.Validation);

        public static Error ProdutosNaoEncontrados =>
            new Error(
                "Nenhum dos produtos informados foi encontrado no cardápio.",
                ErrorType.NotFound);
    }
}