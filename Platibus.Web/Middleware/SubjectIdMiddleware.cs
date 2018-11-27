using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Platibus.Web.Middleware
{
    public class SubjectIdMiddleware
    {
        private readonly RequestDelegate _next;
        
        public SubjectIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext httpContext)
        {
            await _next(httpContext);
        }
    }
}