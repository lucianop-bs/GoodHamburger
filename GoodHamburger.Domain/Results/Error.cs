namespace GoodHamburger.Domain.Results
{
    public record Error(string Message, ErrorType ErrorType)
    {
        public static readonly Error None = new(string.Empty, default);
    }
}