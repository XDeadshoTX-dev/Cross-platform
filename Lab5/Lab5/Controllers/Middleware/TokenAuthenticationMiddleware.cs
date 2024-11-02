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
            if (path.ToString() == "/control" || path.ToString() == "/lab1" || path.ToString() == "/lab2" || path.ToString() == "/lab3" || path.ToString() == "/profile" || path.ToString() == "/controlapi")
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
