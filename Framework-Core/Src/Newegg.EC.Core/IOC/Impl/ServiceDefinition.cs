﻿using System;

namespace Newegg.EC.Core.IOC.Impl
{
    /// <summary>
    /// Service definition.
    /// </summary>
    public class ServiceDefinition : IServiceDefinition
    {
        public ServiceDefinition(Type serivceType, object instance)
        {
            this.ServiceType = serivceType;
            this.ImplementInstance = instance;
        }

        public ServiceDefinition(Type serivceType, Func<IServiceProvider, object> factory, ServiceLifeTime lifeTime)
        {
            this.ServiceType = serivceType;
            this.ImplementFactory = factory;
            this.LifeTime = lifeTime;
        }

        public ServiceDefinition(Type serivceType, Type implementType, ServiceLifeTime lifeTime)
        {
            this.ServiceType = serivceType;
            this.ImplementType = implementType;
            this.LifeTime = lifeTime;
        }

        public Type ServiceType { get; }

        public Type ImplementType { get; }

        public object ImplementInstance { get;}

        public Func<IServiceProvider, object> ImplementFactory { get; }

        public ServiceLifeTime LifeTime { get; }
    }
}
