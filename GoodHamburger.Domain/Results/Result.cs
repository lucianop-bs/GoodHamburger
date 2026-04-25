using GoodHamburger.Domain.Errors;

namespace GoodHamburger.Domain.Results
{
    public class Result<T> : IResult
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyCollection<Error> Errors { get; }

        public Error Error { get; }

        private readonly T? _value;

        private Result(bool isSuccess, T? value, Error error, IReadOnlyCollection<Error>? errors = null)
        {
            IsSuccess = isSuccess;
            _value = value;
            Error = error;
            Errors = errors ?? new List<Error> { error };
        }

        public object GetValue() => _value!;

        public static Result<T> Success(T value) => new Result<T>(true, value, Error.None);

        public static Result<T> Failure(Error error) => new Result<T>(false,default, error);
        public static Result<T> Failure(IReadOnlyCollection<Error> errors) => new Result<T>(false, default, errors.First(), errors);
    }
}