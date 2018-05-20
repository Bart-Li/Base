using Newegg.EC.Core.Host;
using Newegg.EC.Core.Web.Context;

namespace Newegg.EC.Core.Context.Impl
{
    /// <summary>
    /// Default request context.
    /// </summary>
    [AutoSetupService(typeof(IRequestContext))]
    public class DefaultRequestContext : IRequestContext
    {
        /// <summary>
        /// The HTTP context.
        /// </summary>
        private readonly IHttpContextRepository _httpContext;

        /// <summary>
        /// Servers mapping repository.
        /// </summary>
        private readonly IServerMappingRepository _serverMappingRepository;

        public DefaultRequestContext(IHttpContextRepository httpContext, IServerMappingRepository serverMappingRepository)
        {
            this._httpContext = httpContext;
            this._serverMappingRepository = serverMappingRepository;
        }

        /// <summary>
        /// Gets current url.
        /// </summary>
        public string CurrentUrl => this._httpContext.CurrentUrl;

        /// <summary>
        /// Gets the name of the client server.
        /// </summary>
        /// <value>The name of the client server.</value>
        public string ClientServerName => this._serverMappingRepository.CurrentServerName;

        /// <summary>
        /// Gets customer ip address.
        /// </summary>
        public string CustomerIP
        {
            get
            {
                var result = this.GetValueFromContext("X-Source-IP");
                if (string.IsNullOrWhiteSpace(result) && this._httpContext != null)
                {
                    result = this._httpContext.CurrentIP;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets client channel.
        /// </summary>
        public string ClientChannel
        {
            get
            {
                var result = this.GetValueFromContext("X-Channel");
                if (string.IsNullOrWhiteSpace(result) && this._serverMappingRepository != null)
                {
                    result = this._serverMappingRepository.CurrentChannel;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets queried db name.
        /// </summary>
        public string QueriedDbName
        {
            get
            {
                var result = this.GetValueFromContext("X-DataBase");
                if (string.IsNullOrWhiteSpace(result) && this._serverMappingRepository != null)
                {
                    result = this._serverMappingRepository.CurrentServerDatabase;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets queried history db name.
        /// </summary>
        public string QueriedHistoryDbName
        {
            get
            {
                var result = this.GetValueFromContext("X-HisQuery");
                if (string.IsNullOrWhiteSpace(result) && this._serverMappingRepository != null)
                {
                    result = this._serverMappingRepository.CurrentServerHistoryDatabase;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets client url.
        /// </summary>
        public string ClientUrl
        {
            get
            {
                var result = this.GetValueFromContext("X-Client-Url");
                if (string.IsNullOrWhiteSpace(result) && this._httpContext != null)
                {
                    result = this._httpContext.CurrentUrl;
                }

                return result;
            }
        }

        /// <summary>
        /// Gets client url referrer.
        /// </summary>
        public string ClientUrlReferrer
        {
            get
            {
                var result = this.GetValueFromContext("X-Client-UrlReferrer");
                if (string.IsNullOrWhiteSpace(result) && this._httpContext != null)
                {
                    result = this._httpContext.CurrentUrlReferrer;
                }

                return result;
            }
        }   

        /// <summary>
        /// Gets client user agent.
        /// </summary>
        public string ClientUserAgent
        {
            get
            {
                var result = this.GetValueFromContext("X-Client-UserAgent");
                if (string.IsNullOrWhiteSpace(result) && this._httpContext != null)
                {
                    result = this._httpContext.CurrentUserAgent;
                }

                return result;
            }
        }

        /// <summary>
        /// Get string from context.
        /// </summary>
        /// <param name="key">Request key.</param>
        /// <returns>Request result.</returns>
        private string GetValueFromContext(string key)
        {
            string result = string.Empty;
            if (this._httpContext != null)
            {
                result = this._httpContext.QueryStringOrHeader(key);
            }

            return result;
        }
    }
}
