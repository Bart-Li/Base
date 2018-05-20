using System.Collections.Generic;
using Newegg.EC.Core.Host.Config;

namespace Newegg.EC.Core.Host
{
    /// <summary>
    /// Server host repository.
    /// </summary>
    public interface IServiceHostRepository
    {
        /// <summary>
        /// Get service host by service name.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <returns>Service host.</returns>
        ServiceHostUnit GetService(string serviceName);

        /// <summary>
        /// Get all service host by service name.
        /// </summary>
        /// <param name="serviceName">Service name.</param>
        /// <returns>All service host.</returns>
        IList<ServiceHostUnit> GetAllService(string serviceName);
    }
}
