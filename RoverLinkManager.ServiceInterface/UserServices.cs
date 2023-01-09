using RoverLinkManager.Infrastructure.Common.IdGenerator.Models;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Services;
using RoverLinkManager.Infrastructure.UserManager.Services;
using ServiceStack;
using RoverLinkManager.ServiceModel;
using ServiceStack.Auth;

namespace RoverLinkManager.ServiceInterface;

[Authenticate]
public class UserServices : Service
{
    private IdGeneratorService _idGenerator;
    private UserManagerService _userManager;

    // Example of using IOC to get the stream connection factory
    public UserServices(IdGeneratorService idGenerator, UserManagerService userManager)
    {
        _idGenerator = idGenerator;
        _userManager = userManager;
    }

    public object Any(Hello request)
    {
        SnowflakeId snowflake = _idGenerator.CreateId();

        var test = _idGenerator.ToSnowflakeId(snowflake.ShortId);

        IAuthSession session = this.GetSession();
        
        return new HelloResponse { Result = $"Hello, {request.Name}! Your snowflake id is {snowflake.Id}, with a shortId of {snowflake.ShortId}" };
    }
}