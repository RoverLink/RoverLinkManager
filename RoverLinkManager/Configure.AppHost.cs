using System.Text;
using System.Text.Json;
using Funq;
using RoverLinkManager.Domain.Entities.Secrets;
using ServiceStack;
using RoverLinkManager.ServiceInterface;
using RoverLinkManager.Domain.Entities.Settings;
using RoverLinkManager.Infrastructure.GetStream.Services;

[assembly: HostingStartup(typeof(RoverLinkManager.AppHost))]

namespace RoverLinkManager;

public class AppHost : AppHostBase, IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) => {
            // Configure ASP.NET Core IOC Dependencies
        });

    public AppHost() : base("RoverLink Manager", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });

        container.AddTransient<StreamConnectionFactory>();
    }
}
