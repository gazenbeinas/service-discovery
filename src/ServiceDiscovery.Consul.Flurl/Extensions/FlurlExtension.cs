using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Flurl.Http;
using ServiceDiscovery.Consul.Exceptions;

namespace ServiceDiscovery.Consul.Flurl.Extensions
{
    public static class FlurlExtension
    {
        public static IFlurlRequest UseServiceDiscovery(
            this string baseUrl)
        {
            var servicesToDiscoveryMatch = Regex.Match(baseUrl, @"{(.+?)}");

            if (servicesToDiscoveryMatch.Success)
            {
                var serviceToDiscovery = servicesToDiscoveryMatch.Groups[1].Value;

                baseUrl = ResolverServiceName(
                    serviceToDiscovery, baseUrl).Result;
            }

            return baseUrl.ConfigureRequest(x => { });
        }

        private static async Task<string> ResolverServiceName(
            string serviceToDiscovery, string baseUrl)
        {
            var serviceInformation =
                await ServiceDiscoveryHandler
                    .RecoverServiceInformation(serviceToDiscovery);

            if (serviceInformation == null)
                throw new ServiceDiscoveryException(
                    $"Given service name '{serviceToDiscovery}' was not found on Consul catalog");

            return Regex.Replace(baseUrl, @"\{.*?}",
                $"{serviceInformation.Address}:{serviceInformation.Port}");
        }
    }
}