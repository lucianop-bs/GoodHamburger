using GoodHamburger.Domain.Results;

namespace GoodHamburger.Domain.Errors
{
    public static class PedidoError
    {
        public static Error PedidoComItemDuplicado => new Error("Não é permitido adicionar mais de um produto da mesma categoria.", ErrorType.Validation);
    }
}