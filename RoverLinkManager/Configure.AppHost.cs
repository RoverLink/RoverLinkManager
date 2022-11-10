using Funq;
using RoverLinkManager.Infrastructure.Secrets.AWS;
using RoverLinkManager.Infrastructure.Secrets.AWS.Models;
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

    public AppHost() : base("RoverLinkManager", typeof(MyServices).Assembly) {}

    public override void Configure(Container container)
    {
        // Configure ServiceStack only IOC, Config & Plugins
        SetConfig(new HostConfig {
            UseSameSiteCookies = true,
        });

        // Get the aws app secrets configuration from appsettings
        var secretConfig = AppSettings.Get<SecretIdentifier>("AwsConfigurationSecrets");

        // Retrieve the secrets configuration from aws secrets
        var settings = SecretsManager.GetSecret<Secrets>(secretConfig.Name, secretConfig.Region);

        if (settings is null)
        {
            Log.Fatal("Unable to retrieve aws secrets for application settings");

            throw new Exception("Unable to retrieve aws application settings.");
        }

        container.AddSingleton<Secrets>(settings ?? new());
    }
}
