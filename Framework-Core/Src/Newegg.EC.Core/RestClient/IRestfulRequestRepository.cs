namespace Newegg.EC.Core.RestClient
{
    /// <summary>
    /// Restful request respository.
    /// </summary>
    public interface IRestfulRequestRepository
    {
        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <returns>Restful request.</returns>
        IRestfulRequest Create();

        /// <summary>
        /// Create new restful request.
        /// </summary>
        /// <param name="baseUrl">Service base url.</param>
        /// <returns>Restful request.</returns>
        IRestfulRequest Create(string baseUrl);

        /// <summary>
        /// Get request from configuration.
        /// </summary>
        /// <param name="configuredResourceKey">Configured resource key.</param>
        /// <returns>Restful request.</returns>
        IRestfulRequest GetRequestFromConfig(string configuredResourceKey);
    }
}
