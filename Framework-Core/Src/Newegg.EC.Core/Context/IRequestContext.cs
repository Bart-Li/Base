namespace Newegg.EC.Core.Context
{
    /// <summary>
    /// Request context.
    /// </summary>
    public interface IRequestContext
    {
        /// <summary>
        /// Gets current url.
        /// </summary>
        string CurrentUrl { get; }

        /// <summary>
        /// Gets the name of the client server.
        /// </summary>
        /// <value>The name of the client server.</value>
        string ClientServerName { get; }

        /// <summary>
        /// Gets client channel.
        /// </summary>
        string ClientChannel { get; }

        /// <summary>
        /// Gets customer ip address.
        /// </summary>
        string CustomerIP { get; }

        /// <summary>
        /// Gets queried db name.
        /// </summary>
        string QueriedDbName { get; }

        /// <summary>
        /// Gets queried history db name.
        /// </summary>
        string QueriedHistoryDbName { get; }

        /// <summary>
        /// Gets client url.
        /// </summary>
        string ClientUrl { get; }

        /// <summary>
        /// Gets client url referrer.
        /// </summary>
        string ClientUrlReferrer { get; }

        /// <summary>
        /// Gets client user agent.
        /// </summary>
        string ClientUserAgent { get; }
    }
}
