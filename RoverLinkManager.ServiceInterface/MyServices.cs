using RoverLinkManager.Domain.Extensions;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Models;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Services;
using ServiceStack;
using RoverLinkManager.ServiceModel;
using ServiceStack.Auth;

namespace RoverLinkManager.ServiceInterface;

[Authenticate]
public class MyServices : Service
{
    private IdGeneratorService _idGenerator;

    // Example of using IOC to get the stream connection factory
    public MyServices(IdGeneratorService idGenerator)
    {
        _idGenerator = idGenerator;
    }

    public object Any(Hello request)
    {
        SnowflakeId snowflake = _idGenerator.CreateId();
        
        IAuthSession session = this.GetSession();
        return new HelloResponse { Result = $"Hello, {request.Name}! Your snowflake id is {snowflake.Id}, with a shortId of {snowflake.ShortId}" };
    }
}