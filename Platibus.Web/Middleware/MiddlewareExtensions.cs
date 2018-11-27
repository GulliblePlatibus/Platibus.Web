using Microsoft.AspNetCore.Builder;

namespace Platibus.Web.Middleware
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomAuthLookupMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SubjectIdMiddleware>();
        }
    }
}