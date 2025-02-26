namespace ECommerceBackendTaskAPI.Common.Middlewares
{
    public class GlobalErrorHandlerMiddleware
    {
        RequestDelegate _next;
        private readonly IMediator _mediator;


        public GlobalErrorHandlerMiddleware(RequestDelegate next, IMediator mediator)
        {
            _next = next;
            _mediator = mediator;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                string message = $"Error Occured: {ex.Message}.";
                ErrorCode errorCode = ErrorCode.UnKnown;
                if (ex is BusinessException businessException)
                {
                    message = businessException.Message;
                    errorCode = businessException.ErrorCode;
                }
                var loggingMessage = $"Error Occured: {ex.Message}";
                await _mediator.Send(new AddLogCommand(LogLevels.Error, loggingMessage));
                var result = ResultViewModel.Faliure(errorCode, message);

                await context.Response.WriteAsJsonAsync(result);
            }
        }
    }
}
