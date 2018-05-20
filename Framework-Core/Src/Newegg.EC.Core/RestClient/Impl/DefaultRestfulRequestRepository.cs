using Newegg.EC.Core.Host;

namespace Newegg.EC.Core.RestClient.Impl
{
    /// <summary>
    /// Restful request repository.
    /// </summary>
    [AutoSetupService(typeof(IRestfulRequestRepository))]
    public class DefaultRestfulRequestRepository : IRestfulRequestRepository
    {
        /// <summary>
        /// Service host repository.
        /// </summary>
        private readonly IServiceHostRepository _serviceHostRepository;

        /// <summary>
        /// Restful config repository.
        /// </summary>
        private readonly IRestfulConfigRepository _restfulConfigRepository;

        /// <summary>
        /// Default restful request repository.
        /// </summary>
        /// <param name="serviceHostRepository">Service host repository.</param>
        /// <param name="restfulConfigRepository">Restful config repository.</param>
        public DefaultRestfulRequestRepository(IServiceHostRepository serviceHostRepository, IRestfulConfigRepository restfulConfigRepository)
        {
            this._serviceHostRepository = serviceHostRepository;
            this._restfulConfigRepository = restfulConfigRepository;
        }

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <returns>Restful request.</returns>
        public IRestfulRequest Create()
        {
            return this.Create(string.Empty);
        }

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <param name="baseUrl">Service base url.</param>
        /// <returns>Restful request.</returns>
        public IRestfulRequest Create(string baseUrl)
        {
            return new DefaultRestfulRequest {BaseUrl = baseUrl};
        }

        /// <summary>
        /// Get request from configuration.
        /// </summary>
        /// <param name="configuredResourceKey">Configured resource key.</param>
        /// <returns>Restful request.</returns>
        public IRestfulRequest GetRequestFromConfig(string configuredResourceKey)
        {
            return new ConfiguredRestfulRequest(configuredResourceKey, this._serviceHostRepository, this._restfulConfigRepository);
        }
    }
}
