using System;

namespace ServiceDiscovery.Consul.Exceptions
{
    public class ServiceDiscoveryException : Exception
    {
        public ServiceDiscoveryException(string message)
            : base(message)
        {
        }
    }
}