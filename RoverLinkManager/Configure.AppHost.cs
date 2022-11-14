using System.Text;
using System.Text.Json;
using Funq;
using RoverLinkManager.Infrastructure.Secrets.AWS;
using ServiceStack;
using RoverLinkManager.ServiceInterface;
using RoverLinkManager.Domain.Entities.Settings;

[assembly: HostingStartup(typeof(RoverLinkManager.AppHost))]

namespace RoverLinkManager;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices(services => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("RoverLink Manager", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });

        //AppSettings.Set<Secrets>("Secrets", settings);
        //container.AddSingleton<Secrets>(settings ?? new());
    }
}
