using System.Text;

namespace ECommerceBackendTaskAPI.Common.Behaviors
{
    public class ValidationPipelineBehavior<TRequest, TResponse>
     : IPipelineBehavior<TRequest, TResponse>
     where TRequest : IRequest<TResponse>

    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationFailures = await Task.WhenAll(
                    _validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));


                var errors = validationFailures
                    .SelectMany(validationResult => validationResult.Errors)
                     .Where(validationFailure => validationFailure is not null)
                     .Select(failure => new
                     {
                         failure.PropertyName,
                         failure.ErrorMessage
                     })
                     .ToArray();

                if (errors.Any())
                {
                    StringBuilder message = new StringBuilder();
                    foreach (var item in errors.Select(e => $"{e.PropertyName}: {e.ErrorMessage}"))
                    {
                        message.AppendLine(item);
                    }
                    throw new RequstValidationException(message.ToString());
                }
                return await next();
            }


            return await next();
        }


    }
}
