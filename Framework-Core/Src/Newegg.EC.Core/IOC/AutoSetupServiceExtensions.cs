using Microsoft.Extensions.DependencyInjection;

namespace Newegg.EC.Core.IOC
{
    /// <summary>
    /// Auto setup service extensions.
    /// </summary>
    public static class AutoSetupServiceExtensions
    {
        /// <summary>
        /// Add auto setup service.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        internal static IServiceCollection AddAutoSetupService(this IServiceCollection services)
        {
            return ECLibraryContainer.Current.RegisterService(services);
        }
    }
}
