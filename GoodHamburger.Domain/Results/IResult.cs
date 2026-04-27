namespace GoodHamburger.Domain.Results
{
    public interface IResult
    {
        bool IsSuccess { get; }

        bool IsFailure { get; }

        public IReadOnlyCollection<Error> Errors { get; }

        public Error? Error { get; }

        object GetValue();
    }
}