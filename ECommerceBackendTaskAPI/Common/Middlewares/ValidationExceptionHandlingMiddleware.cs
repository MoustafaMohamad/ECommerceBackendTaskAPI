namespace ECommerceBackendTaskAPI.Common.Middlewares
{
    public class ValidationExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidationExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (RequstValidationException exception)
            {
                string message = exception.Message;
                var result = ResultViewModel.Faliure(ErrorCode.BadRequest, message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
