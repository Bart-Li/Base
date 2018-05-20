using System;
using System.Collections.Generic;
using System.Text;

namespace Newegg.EC.Core.IOC
{
    /// <summary>
    /// Service definition
    /// </summary>
    public interface IServiceDefinition
    {
        /// <summary>
        /// Gets service type.
        /// </summary>
        Type ServiceType { get; }

        /// <summary>
        /// Gets implementation type.
        /// </summary>
        Type ImplementType { get; }

        /// <summary>
        /// Gets implementation instance.
        /// </summary>
        object ImplementInstance { get; }

        /// <summary>
        /// Gets service factory.
        /// </summary>
        Func<IServiceProvider, object> ImplementFactory { get; }

        /// <summary>
        /// Gets service life time.
        /// </summary>
        ServiceLifeTime LifeTime { get; }
    }
}
