# ServiceDiscovery.Consul.Flurl

* consuldotnet: [v0.7.2.6](https://github.com/PlayFab/consuldotnet/tree/0.7.2.6)
* Flurl.Http: [v2.4.0](https://github.com/tmenier/Flurl/tree/Flurl.2.4.0)

ServiceDiscovery.Consul.Flurl is a .NET package for Service Discovery work via Consul.
 It basically formats your given path from a string and returns a IFlurlRequest object 
 with the appropriated IP Address and service port.

## Example

Add a reference to the ServiceDiscovery.Consul.Flurl library:

Call the method SetUp from ServiceDiscoverySetUp to configure the ServiceDiscovery with your Consul address:

```csharp
public void Configure()
{
    ServiceDiscoverySetUp.SetUp(() => new ServiceDiscoveryConfiguration
    {
		Address = "http://127.0.0.1:8500"
	});
}
```

Use it as you would use Flurl.Http, but right after your address use .UseServiceDiscovery():

```csharp
    "http://{your-service-name}/api/item/1"
        .UseServiceDiscovery()
        .AllowAnyHttpStatus()
	.GetAsync();
```

Just replace `your-service-name` with your service name from Consul and that is all.