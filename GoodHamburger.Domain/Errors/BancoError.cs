using GoodHamburger.Domain.Results;

namespace GoodHamburger.Domain.Errors
{
    public static class BancoError
    {
        public static Error TransacaoFalhou =>
            new Error("Falha ao salvar as alterações no banco de dados.",
                ErrorType.Failure);
    }
}