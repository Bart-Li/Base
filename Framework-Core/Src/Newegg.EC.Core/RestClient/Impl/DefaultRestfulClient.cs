using System.Threading.Tasks;

namespace Newegg.EC.Core.RestClient.Impl
{
    /// <summary>
    /// Default restful client.
    /// </summary>
    [AutoSetupService(typeof(IRestfulClient))]
    internal class DefaultRestfulClient : IRestfulClient
    {
        private readonly IRestfulRequestRepository _restfulRequestRepository;

        private readonly IRestfulHttpClient _restfulHttpClient;

        /// <summary>
        /// Default restful client.
        /// </summary>
        /// <param name="restfulRequestRepository">Restful request repository.</param>
        /// <param name="restfulHttpClient">Restful http client.</param>
        public DefaultRestfulClient(IRestfulRequestRepository restfulRequestRepository, IRestfulHttpClient restfulHttpClient)
        {
            this._restfulRequestRepository = restfulRequestRepository;
            this._restfulHttpClient = restfulHttpClient;
        }

        #region Create Restful Request

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <returns>Restful request.</returns>
        public IRestfulRequest Create()
        {
            return this._restfulRequestRepository.Create();
        }

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <param name="url">Service url.</param>
        /// <returns>Restful request.</returns>
        public IRestfulRequest Create<TRequest>(string url)
        {
            return this._restfulRequestRepository.Create(url);
        }

        /// <summary>
        /// Get restful request from configuration.
        /// </summary>
        /// <param name="resourceKey">Resource key.</param>
        /// <returns>Restful request.</returns>
        public IRestfulRequest GetRequestFromConfig(string resourceKey)
        {
            return this._restfulRequestRepository.GetRequestFromConfig(resourceKey);
        }

        #endregion

        #region Send request sync

        /// <summary>
        /// Send request sync.
        /// </summary>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public IRestfulResponse Send(IRestfulRequest request)
        {
            return this._restfulHttpClient.Send(request);
        }

        /// <summary>
        /// Send request sync.
        /// </summary>
        /// <typeparam name="TResponse">Type of response body.</typeparam>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public IRestfulResponse<TResponse> Send<TResponse>(IRestfulRequest request)
        {
            return this._restfulHttpClient.Send<TResponse>(request);
        }

        #endregion

        #region Send request async

        /// <summary>
        /// Send request async.
        /// </summary>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public Task<IRestfulResponse> SendAsync(IRestfulRequest request)
        {
            return this._restfulHttpClient.SendAsync(request);
        }

        /// <summary>
        /// Send request async.
        /// </summary>
        /// <typeparam name="TResponse">Type of response body.</typeparam>
        /// <param name="request">Restful request.</param>
        /// <returns>Restful response.</returns>
        public Task<IRestfulResponse<TResponse>> SendAsync<TResponse>(IRestfulRequest request)
        {
            return this._restfulHttpClient.SendAsync<TResponse>(request);
        }

        #endregion
    }
}
