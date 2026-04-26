namespace GoodHamburger.Domain.Results
{
    public class Result<T> : Result
    {
        private readonly T? _value;

        private Result(bool isSuccess, T? value, Error? error, IReadOnlyCollection<Error>? errors = null) : base(isSuccess, error, errors)
        {
            _value = value;
        }

        public override object GetValue() => _value!;

        public static Result<T> Success(T value) => new Result<T>(true, value, Results.Error.None);

        public static Result<T> Failure(Error error) => new Result<T>(false, default, error);

        public static Result<T> Failure(IReadOnlyCollection<Error> errors)
        {
            return new Result<T>(false, default, errors.FirstOrDefault(), errors);
        }
    }

    public class Result : IResult
    {
        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public IReadOnlyCollection<Error> Errors { get; }

        public Error? Error { get; }

        protected Result(bool isSuccess, Error? error, IReadOnlyCollection<Error>? errors = null)
        {
            IsSuccess = isSuccess;
            Error = error;
            Errors = errors ?? new List<Error> { error };
        }

        public virtual object GetValue() => null;

        public static Result Success() => new Result(true, Results.Error.None);

        public static Result Failure(Error error) => new Result(false, error);
    }
}