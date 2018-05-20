using System;
using Newegg.EC.Core.RestClient.Config;

namespace Newegg.EC.Core.RestClient
{
    /// <summary>
    /// Restful service config repository.
    /// </summary>
    public interface IRestfulConfigRepository
    {
        /// <summary>
        /// Get default timeout.
        /// </summary>
        /// <returns>Default timeout.</returns>
        TimeSpan DefaultTimeout { get; }

        /// <summary>
        /// Get default max response size.
        /// </summary>
        /// <returns>Default response size.</returns>
        long DefaultMaxResponseSize { get; }

        /// <summary>
        /// A value indicating whether remove default parameters.
        /// </summary>
        bool RemoveDefaultParameter { get; }

        /// <summary>
        /// Get restful service resource unit.
        /// </summary>
        /// <param name="resourceKey">Resource key.</param>
        /// <returns>Restful service resource unit.</returns>
        RestfulServiceResourceUnit GetResource(string resourceKey);

        /// <summary>
        /// Get restful service setting unit.
        /// </summary>
        /// <param name="settingKey">Setting key.</param>
        /// <returns>Restfu service setting unit.</returns>
        RestfulServiceSettingUnit GetSetting(string settingKey);


        /// <summary>
        /// Get restful service resource unit.
        /// </summary>
        /// <param name="url">Resource url.</param>
        /// <returns>Restful service resource unite.</returns>
        RestfulServiceResourceUnit GetResourceByUrl(string url);
    }
}
