namespace ECommerceBackendTaskAPI.Common.Middlewares
{
    public class TransactionMiddleware
    {
        private RequestDelegate _next;

        public TransactionMiddleware(RequestDelegate next)
        {

            _next = next;

        }

        public async Task InvokeAsync(HttpContext context, Context dbContext)
        {
            Console.WriteLine($"{this.GetHashCode()}:: -----------------------");
            string method = context.Request.Method.ToUpper();

            if (method == "POST" || method == "PUT" || method == "DELETE")
            {
                var transaction = await dbContext.Database.BeginTransactionAsync();
                try
                {
                    await _next(context);
                    await transaction.CommitAsync();
                    await dbContext.SaveChangesAsync();


                }
                catch (Exception )
                {

                   await transaction.RollbackAsync();
                    throw;
                }
            }

            else
            {

                await _next(context);
            }
        }



    }
}
