using System;
using System.Linq;
using System.Threading.Tasks;
using Consul;

namespace ServiceDiscovery.Consul
{
    public class ServiceDiscoveryHandler
    {
        static Random _random;

        private static IConsulClient _consulClient;

        private ServiceDiscoveryHandler()
        {
        }

        static ServiceDiscoveryHandler()
        {
            _random = new Random();
        }

        public static void Initialize(IConsulClient consulClient) =>
            _consulClient = consulClient;

        public static async Task<ServiceInformation> RecoverServiceInformation(
            string serviceName)
        {
            try
            {
                var serviceResult = await _consulClient.Catalog
                    .Service(serviceName);

                if (!serviceResult.Response.Any())
                    return null;

                var randomService = serviceResult.Response[
                    _random.Next(0, serviceResult.Response.Length - 1)];

                return new ServiceInformation(
                    serviceName,
                    randomService.Address,
                    randomService.ServicePort);
            }
            catch (Exception e)
            {
                throw new Exception(
                    "An error occurred while recovering address of " +
                    $"'{serviceName}' - {e.Message}");
            }
        }
    }
}