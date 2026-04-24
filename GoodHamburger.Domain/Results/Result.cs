namespace GoodHamburger.Domain.Results
{
    public class Result<T> : IResult
    {
        public bool IsSuccess { get; }

        public bool isFailure => !IsSuccess;

        public string ErrorMessage { get; }

        public ErrorType ErrorType { get; }

        private readonly T? _value;

        private Result(bool isSuccess, T? value, string errorMessage, ErrorType errorType)
        {
            IsSuccess = isSuccess;
            _value = value;
            ErrorMessage = errorMessage;
            ErrorType = errorType;
        }

        public object GetValue() => _value!;

        public static Result<T> Success(T value) => new Result<T>(true, value, string.Empty, default);

        public static Result<T> Failure(string errorMessage, ErrorType errorType) => new Result<T>(false, default, errorMessage, errorType);
    }
}