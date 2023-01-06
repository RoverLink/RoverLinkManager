using System.Threading.Tasks;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Models;
using RoverLinkManager.Infrastructure.Common.IdGenerator.Services;
using RoverLinkManager.Infrastructure.LinkManager.Services;
using ServiceStack;
using RoverLinkManager.ServiceModel;
using ServiceStack.Auth;

namespace RoverLinkManager.ServiceInterface;

public class LinkServices : Service
{
    private readonly LinkManagerService _linkManager;

    public LinkServices(LinkManagerService linkManager)
    {
        _linkManager = linkManager;

        // var link = _linkManager.CreateShortLink("https://scholarships.eastonsd.org/").Result;
    }

    public async Task<object> Any(LinkRequest request)
    {
        var link = await _linkManager.GetShortLinkAsync(request.ShortId, true);

        if (link == null) 
            return new {};

        return HttpResult.Redirect(link.Url);

    }
}