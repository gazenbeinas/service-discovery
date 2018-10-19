namespace ServiceDiscovery.Consul
{
    public class ServiceInformation
    {
        public string ServiceName { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }

        public ServiceInformation(
            string serviceName,
            string address,
            int port)
        {
            ServiceName = serviceName;
            Address = address;
            Port = port;
        }
    }
}