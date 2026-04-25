using GoodHamburger.Domain.Results;

namespace GoodHamburger.Domain.Errors
{
    public record Error(string Message, ErrorType ErrorType)
    {
        public static readonly Error None = new(string.Empty, ErrorType.Failure);
    }

}
