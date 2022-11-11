using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoverLinkManager.Infrastructure.Secrets.AWS.Models;
using ServiceStack;
using ServiceStack.Admin;
using ServiceStack.Configuration;
using ServiceStack.Data;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

[assembly: HostingStartup(typeof(RoverLinkManager.ConfigureDb))]

namespace RoverLinkManager;

public class ConfigureDb : IHostingStartup
{
    public void Configure(IWebHostBuilder builder) => builder
        .ConfigureServices((context, services) =>
        {

            var secrets = context.Configuration.Get<Secrets>();

            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                context.Configuration.GetConnectionString("DefaultConnection")
                ?? "Server=localhost;Database=test;User Id=test;Password=test;MultipleActiveResultSets=True;",
                SqlServer2012Dialect.Provider));
        })
        .ConfigureAppHost(appHost => {
            // Enable built-in Database Admin UI at /admin-ui/database
            appHost.Plugins.Add(new AdminDatabaseFeature());
            appHost.Plugins.Add(new AdminUsersFeature());
        });
}
