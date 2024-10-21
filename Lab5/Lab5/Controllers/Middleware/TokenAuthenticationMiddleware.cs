namespace Lab5.Controllers.Middleware
{
    public class TokenAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public TokenAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var path = context.Request.Path.Value.ToLower();
            if (path.Contains("/control") || path.Contains("/lab1") || path.Contains("/lab2") || path.Contains("/lab3") || path.Contains("/profile"))
            {
                var token = context.Request.Cookies["AuthToken"];

                if (string.IsNullOrEmpty(token))
                {
                    context.Response.Redirect("/");
                    return;
                }
            }
            else if (path.ToString() == "/")
            {
                var token = context.Request.Cookies["AuthToken"];
                if (!string.IsNullOrEmpty(token))
                {
                    context.Response.Redirect("/Control");
                    return;
                }
            }
            await _next(context);
        }
    }
}
