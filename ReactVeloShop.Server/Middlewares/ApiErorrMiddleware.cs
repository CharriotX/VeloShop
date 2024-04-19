namespace ReactVeloShop.Server.Middlewares
{
    public class ApiErorrMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiErorrMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UnauthorizedAccessException)
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            }
        }
    }
}
