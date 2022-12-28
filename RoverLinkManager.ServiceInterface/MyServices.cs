using ServiceStack;
using RoverLinkManager.ServiceModel;
using ServiceStack.Auth;

namespace RoverLinkManager.ServiceInterface;

[Authenticate]
public class MyServices : Service
{
    public object Any(Hello request)
    {
	    IAuthSession session = this.GetSession();
        return new HelloResponse { Result = $"Hello, {request.Name}!" };
    }
}