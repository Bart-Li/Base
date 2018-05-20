using System;

namespace Newegg.EC.Core
{
    /// <summary>
    /// Class with this attribute will be auto register as a service,Default lift time is singleton.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AutoSetupServiceAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the AutoSetupServiceAttribute class.
        /// </summary>
        /// <param name="service">Service type.</param>
        public AutoSetupServiceAttribute(Type service)
        {
            this.Service = service;
            this.LifeTime = ServiceLifeTime.Singleton;
        }

        /// <summary>
        /// Gets service type.
        /// </summary>
        public Type Service { get; private set; }

        /// <summary>
        /// Gets or sets life time.
        /// </summary>
        public ServiceLifeTime LifeTime { get; set; }

        /// <summary>
        /// Gets or sets priority.
        /// </summary>
        public int Priority { get; set; }
    }
}
