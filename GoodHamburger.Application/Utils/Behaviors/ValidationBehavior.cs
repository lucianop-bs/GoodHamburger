using FluentValidation;
using GoodHamburger.Domain.Results;
using MediatR;

namespace GoodHamburger.Application.Utils.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse>
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var failures = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count == 0)
                return await next();

            var errors = failures
                .Select(f => new Error(f.ErrorMessage, ErrorType.Validation))
                .ToList();

            if (typeof(TResponse) == typeof(Result))
                return (TResponse)(object)Result.Failure(errors[0]);

            var innerType = typeof(TResponse).GetGenericArguments()[0];

            var resultGenericType = typeof(Result<>).MakeGenericType(innerType);

            var failureMethod = resultGenericType.GetMethod(
                "Failures",
                System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static,
                binder: null,
                types: new[] { typeof(Error) },
                modifiers: null);

            var result = failureMethod!.Invoke(null, new object[] { errors[0] });

            return (TResponse)result!;
        }
    }
}