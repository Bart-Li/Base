using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newegg.EC.Core.Web.Context;

namespace Newegg.EC.Core
{
    public static class StaticHttpContextExtensions
    {
        /// <summary>
        /// Use static http context.
        /// </summary>
        /// <param name="app">Application builder.</param>
        /// <returns>Application builder.</returns>
        public static IApplicationBuilder UseStaticHttpContext(this IApplicationBuilder app)
        {
            var httpContextAccessor = app.ApplicationServices.GetRequiredService<IHttpContextAccessor>();
            HttpCurrentContext.Configure(httpContextAccessor);
            return app;
        }
    }
}
