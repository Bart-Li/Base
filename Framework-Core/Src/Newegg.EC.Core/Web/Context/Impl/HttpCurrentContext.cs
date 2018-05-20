using Microsoft.AspNetCore.Http;

namespace Newegg.EC.Core.Web.Context
{
    public class HttpCurrentContext
    {
        private static IHttpContextAccessor _accessor;

        public static HttpContext Current => _accessor.HttpContext;

        internal static void Configure(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
    }
}
