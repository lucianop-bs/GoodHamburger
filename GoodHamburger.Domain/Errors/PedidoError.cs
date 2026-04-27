using GoodHamburger.Domain.Results;

namespace GoodHamburger.Domain.Errors
{
    public static class PedidoError
    {
        public static Error PedidoComItemDuplicado =>
            new Error("Não é permitido adicionar mais de um produto da mesma categoria.",
                ErrorType.Validation);

        public static Error PedidoNaoEncontrado =>
            new Error("Pedido não encontrado",
                ErrorType.NotFound);

        public static Error PedidoEmBranco =>
            new Error("Deve adicionar pelo o menos um item do pedido",
                ErrorType.Validation);

        public static Error IdInvalido =>
            new Error("ID do pedido é inválido.",
                ErrorType.Validation);

        public static Error PedidoNull =>
            new Error("A lista de produtos não pode ser nula.",
                ErrorType.Validation);

        public static Error MaximoDePedidos =>
            new Error("Um pedido pode ter no máximo 3 produtos",
                ErrorType.Validation);
    }
}