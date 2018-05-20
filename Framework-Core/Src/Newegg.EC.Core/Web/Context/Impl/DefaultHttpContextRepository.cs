using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;

namespace Newegg.EC.Core.Web.Context
{
    [AutoSetupService(typeof(IHttpContextRepository))]
    public class DefaultHttpContextRepository : IHttpContextRepository
    {
        /// <summary>
        /// Gets current url.
        /// </summary>
        public string CurrentUrl
        {
            get
            {
                var result = string.Empty;
                if (this.CurrentHttpContext == null)
                {
                    return result;
                }

                var httpRequest = this.CurrentHttpContext.Request;
                if (httpRequest != null)
                {
                    return httpRequest.GetDisplayUrl();
                }

                return result;
            }
        }

        /// <summary>
        /// Gets current url referrer.
        /// </summary>
        public string CurrentUrlReferrer => this.Header("Referer");

        /// <summary>
        /// Gets current user agent.
        /// </summary>
        public string CurrentUserAgent => this.Header("User-Agent");

        /// <summary>
        /// Gets current ip.
        /// </summary>
        public string CurrentIP
        {
            get
            {
                var result = string.Empty;
                if (this.CurrentHttpContext == null)
                {
                    return result;
                }

                return this.CurrentHttpContext.Connection.LocalIpAddress.ToString();
            }
        }

        /// <summary>
        /// Gets current http context.
        /// </summary>
        public HttpContext CurrentHttpContext => HttpCurrentContext.Current;

        /// <summary>
        /// Get value from http request header.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        public string Header(string key)
        {
            var result = string.Empty;
            if (this.CurrentHttpContext == null)
            {
                return result;
            }

            var httpRequest = this.CurrentHttpContext.Request;
            if (httpRequest != null)
            {
                result = httpRequest.Headers[key];
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = string.Empty;
                }
            }

            return result.Trim();
        }

        /// <summary>
        /// Get value from http request query string.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        public string QueryString(string key)
        {
            var result = string.Empty;
            if (this.CurrentHttpContext == null)
            {
                return result;
            }

            var httpRequest = this.CurrentHttpContext.Request;
            if (httpRequest != null)
            {
                result = httpRequest.Query[key];
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = string.Empty;
                }
            }

            return result.Trim();
        }

        /// <summary>
        /// Get value from http request query string or header.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        public string QueryStringOrHeader(string key)
        {
            var result = string.Empty;
            result = this.QueryString(key);
            if (string.IsNullOrEmpty(result))
            {
                result = this.Header(key);
            }

            return result;
        }

        /// <summary>
        /// Gets value from http request cookie.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        public string Cookie(string key)
        {
            var result = string.Empty;
            if (this.CurrentHttpContext == null)
            {
                return result;
            }

            var httpRequest = this.CurrentHttpContext.Request;
            if (httpRequest != null)
            {
                result = httpRequest.Cookies[key];
                if (string.IsNullOrWhiteSpace(result))
                {
                    result = string.Empty;
                }
            }

            return result.Trim();
        }
    }
}
