using System;
using Microsoft.Extensions.DependencyInjection;
using Newegg.EC.Core.IOC;

namespace Newegg.EC.Core
{
    /// <summary>
    /// Entry point for the container infrastructure for Newegg EC Library.
    /// </summary>
    public class ECLibraryContainer
    {
        /// <summary>
        /// The current instance.
        /// </summary>
        static ECLibraryContainer _currentInstance;

        /// <summary>
        /// The framework service collection.
        /// </summary>
        private IServiceCollection _servicesCollection;

        /// <summary>
        /// The framework service provider.
        /// </summary>
        private IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of the ECLibraryContainer class.
        /// </summary>
        internal ECLibraryContainer()
        {
            _currentInstance = this;
        }

        /// <summary>
        /// Gets the current instance.
        /// </summary>
        /// <value>The current.</value>
        public static ECLibraryContainer Current => _currentInstance ?? new ECLibraryContainer();

        /// <summary>
        /// Add auto setup service.
        /// </summary>
        /// <param name="services">Service collection.</param>
        /// <returns>Service collection.</returns>
        public IServiceCollection RegisterService(IServiceCollection services)
        {
            this._servicesCollection = services;
            this._servicesCollection.AddSingleton(services);
            var serviceDefinitionCollection = ServiceRepository.BuildServiceDefinitionCollection();
            if (serviceDefinitionCollection != null && !serviceDefinitionCollection.ServiceDefinitionCollection.IsNullOrEmpty())
            {
                serviceDefinitionCollection.ServiceDefinitionCollection.ForEach(definition =>
                {
                    if (definition.LifeTime == ServiceLifeTime.Scope)
                    {
                        RegisterScopedService(definition);
                    }
                    else if (definition.LifeTime == ServiceLifeTime.Singleton)
                    {
                        RegisterSingletonService(definition);
                    }
                    else if (definition.LifeTime == ServiceLifeTime.Transient)
                    {
                        RegisterTransientService(definition);
                    }
                });
            }

            this._serviceProvider = this._servicesCollection.BuildServiceProvider();

            return services;
        }

        /// <summary>
        /// Register singleton service.
        /// </summary>
        /// <param name="definition">Service definition.</param>
        private void RegisterSingletonService(IServiceDefinition definition)
        {
            if (definition.ImplementInstance != null)
            {
                this._servicesCollection.AddSingleton(definition.ServiceType, definition.ImplementInstance.GetType());
            }
            else if (definition.ImplementFactory != null)
            {
                this._servicesCollection.AddSingleton(definition.ServiceType, definition.ImplementFactory);
            }
            else if (definition.ImplementType != null)
            {
                this._servicesCollection.AddSingleton(definition.ServiceType, definition.ImplementType);
            }
        }

        /// <summary>
        /// Register scoped service.
        /// </summary>
        /// <param name="definition">Service definition.</param>
        private void RegisterScopedService(IServiceDefinition definition)
        {
            if (definition.ImplementInstance != null)
            {
                this._servicesCollection.AddScoped(definition.ServiceType, definition.ImplementInstance.GetType());
            }
            else if (definition.ImplementFactory != null)
            {
                this._servicesCollection.AddScoped(definition.ServiceType, definition.ImplementFactory);
            }
            else if (definition.ImplementType != null)
            {
                this._servicesCollection.AddScoped(definition.ServiceType, definition.ImplementType);
            }
        }

        /// <summary>
        /// Register transient service.
        /// </summary>
        /// <param name="definition">Service definition.</param>
        private void RegisterTransientService(IServiceDefinition definition)
        {
            if (definition.ImplementInstance != null)
            {
                this._servicesCollection.AddTransient(definition.ServiceType, definition.ImplementInstance.GetType());
            }
            else if (definition.ImplementFactory != null)
            {
                this._servicesCollection.AddTransient(definition.ServiceType, definition.ImplementFactory);
            }
            else if (definition.ImplementType != null)
            {
                this._servicesCollection.AddTransient(definition.ServiceType, definition.ImplementType);
            }
        }

        /// <summary>
        /// Get service from collection.
        /// </summary>
        /// <param name="serviceType">Service type.</param>
        /// <returns>Service instance.</returns>
        public static object GetService(Type serviceType)
        {
            return Current._serviceProvider.GetService(serviceType);
        }

        /// <summary>
        /// Get service from collection.
        /// </summary>
        /// <typeparam name="TServiceType">Service type.</typeparam>
        /// <returns>Service instance.</returns>
        public static TServiceType Get<TServiceType>() where TServiceType : class
        {
            return Current._serviceProvider.GetService<TServiceType>();
        }
    }
}
