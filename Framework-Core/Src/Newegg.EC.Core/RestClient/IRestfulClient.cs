using System.Threading.Tasks;

namespace Newegg.EC.Core.RestClient
{
    /// <summary>
    /// Restful client interface.
    /// </summary>
    public interface IRestfulClient
    {
        #region Create Restful Request

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <returns>Restful request.</returns>
        IRestfulRequest Create();

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <param name="url">Service url.</param>
        /// <returns>Restful request.</returns>
        IRestfulRequest Create<TRequest>(string url);

        /// <summary>
        /// Get restful request from configuration.
        /// </summary>
        /// <param name="resourceKey">Resource key.</param>
        /// <returns>Restful request.</returns>
        IRestfulRequest GetRequestFromConfig(string resourceKey);

        #endregion

        #region Send request sync

        /// <summary>
        /// Send request sync.
        /// </summary>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        IRestfulResponse Send(IRestfulRequest request);

        /// <summary>
        /// Send request sync.
        /// </summary>
        /// <typeparam name="TResponse">Type of response body.</typeparam>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        IRestfulResponse<TResponse> Send<TResponse>(IRestfulRequest request);

        #endregion

        #region Send request async

        /// <summary>
        /// Send request async.
        /// </summary>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        Task<IRestfulResponse> SendAsync(IRestfulRequest request);

        /// <summary>
        /// Send request async.
        /// </summary>
        /// <typeparam name="TResponse">Type of response body.</typeparam>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        Task<IRestfulResponse<TResponse>> SendAsync<TResponse>(IRestfulRequest request);

        #endregion
    }
}
