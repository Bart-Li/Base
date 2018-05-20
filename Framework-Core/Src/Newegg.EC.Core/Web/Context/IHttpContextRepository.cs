using Microsoft.AspNetCore.Http;

namespace Newegg.EC.Core.Web.Context
{
    public interface IHttpContextRepository
    {
        /// <summary>
        /// Gets current url.
        /// </summary>
        string CurrentUrl { get; }

        /// <summary>
        /// Gets current url referrer.
        /// </summary>
        string CurrentUrlReferrer { get; }

        /// <summary>
        /// Gets current user agent.
        /// </summary>
        string CurrentUserAgent { get; }

        /// <summary>
        /// Gets current ip.
        /// </summary>
        string CurrentIP { get; }

        /// <summary>
        /// Gets current http context.
        /// </summary>
        HttpContext CurrentHttpContext { get; }

        /// <summary>
        /// Get value from http request header.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        string Header(string key);

        /// <summary>
        /// Get value from http request query string.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        string QueryString(string key);

        /// <summary>
        /// Get value from http request query string or header.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        string QueryStringOrHeader(string key);

        /// <summary>
        /// Gets value from http request cookie.
        /// </summary>
        /// <param name="key">Queried item key.</param>
        /// <returns>Queried result.</returns>
        string Cookie(string key);
    }
}
