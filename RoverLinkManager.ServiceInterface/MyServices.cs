using ServiceStack;
using RoverLinkManager.ServiceModel;

namespace RoverLinkManager.ServiceInterface;

public class MyServices : Service
{
    public object Any(Hello request)
    {
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}