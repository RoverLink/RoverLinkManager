using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RoverLinkManager.Domain.Entities.Secrets;
using RoverLinkManager.Domain.Entities.Settings;
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
        .ConfigureAppConfiguration(build =>
        {
            build.AddSecretsManager(configurator: options =>
            {
	            options.KeyGenerator = (entry, key) =>
	            {
		            return key.SplitOnFirst(':').Last();
	            };
            });
        })
        .ConfigureServices((context, services) =>
        {
	        var settings = context.Configuration.Get<ApplicationSettings>();

            if (settings == null)
            {
                throw new Exception("Unable to retrieve AWS configuration secrets");
            }

            services.AddSingleton<ApplicationSettings>(settings);
            var connString = settings.Database.ToConnectionString();

            services.AddSingleton<IDbConnectionFactory>(new OrmLiteConnectionFactory(
                connString,
                PostgreSqlDialect.Provider));

        })
        .ConfigureAppHost(appHost => {
            // Enable built-in Database Admin UI at /admin-ui/database
            appHost.Plugins.Add(new AdminDatabaseFeature());
            appHost.Plugins.Add(new AdminUsersFeature());
        });
}
