using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware
{
    public class ActionTrackingMiddleware
    {
        private readonly RequestDelegate _next;

        public ActionTrackingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Store the controller action method name
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var routePattern = endpoint.Metadata.GetMetadata<Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor>();
                if (routePattern != null)
                {
                    RequestContext.ActionName = $"{routePattern.ControllerName}.{routePattern.ActionName}";
                }
            }

            await _next(context);
        }
    }

    public static class RequestContext
    {
        private static AsyncLocal<string?> _actionName = new();

        public static string? ActionName
        {
            get => _actionName.Value;
            set => _actionName.Value = value;
        }
    }
}
