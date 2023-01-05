using RoverLinkManager.Infrastructure.GetStream.Services;
using ServiceStack;
using RoverLinkManager.ServiceModel;
using ServiceStack.Auth;

namespace RoverLinkManager.ServiceInterface;

[Authenticate]
public class MyServices : Service
{
    // Example of using IOC to get the stream connection factory
    public MyServices(StreamConnectionFactory stream)
    {

    }

    public object Any(Hello request)
    {
	    IAuthSession session = this.GetSession();
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}