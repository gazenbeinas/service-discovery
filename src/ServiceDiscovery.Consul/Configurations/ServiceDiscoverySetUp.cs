using System;
using Consul;

namespace ServiceDiscovery.Consul.Configurations
{
    public class ServiceDiscoverySetUp
    {
        public static void SetUp(Func<ServiceDiscoveryConfiguration> configurationFunction)
        {
            var configuration = configurationFunction.Invoke();

            var consulClient = new ConsulClient(consulConfig =>
            {
                consulConfig.Address = new Uri(configuration.Address);
            });

            ServiceDiscoveryHandler.Initialize(consulClient);
        }
    }
}