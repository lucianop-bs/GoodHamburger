namespace GoodHamburger.Domain.Results
{
    public interface IResult
    {
        bool IsSuccess { get; }
        bool isFailure { get; }

        string ErrorMessage { get; }
        ErrorType ErrorType { get; }

        object GetValue();
    }
}