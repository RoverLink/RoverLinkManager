using System.Drawing;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Amazon.Runtime.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.JsonWebTokens;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.FluentValidation;
using ServiceStack.Host;
using ServiceStack.Jwks;
using ServiceStack.Text;

[assembly: HostingStartup(typeof(RoverLinkManager.ConfigureAuth))]

namespace RoverLinkManager
{
    // Add any additional metadata properties you want to store in the Users Typed Session
    public class CustomUserSession : AuthUserSession
    {
    }
    
// Custom Validator to add custom validators to built-in /register Service requiring DisplayName and ConfirmPassword
    public class CustomRegistrationValidator : RegistrationValidator
    {
        public CustomRegistrationValidator()
        {
            RuleSet(ApplyTo.Post, () =>
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.ConfirmPassword).NotEmpty();
            });
        }
    }

    public class ConfigureAuth : IHostingStartup
    {
        public string? ValidateJwtPayload(Dictionary<string, string> payload)
        {
	        return null;
        }

        public void Configure(IWebHostBuilder builder) => builder
            .ConfigureServices(services => {
                //services.AddSingleton<ICacheClient>(new MemoryCacheClient()); //Store User Sessions in Memory Cache (default)
            })
            .ConfigureAppHost(appHost => {
                var appSettings = appHost.AppSettings;

		        var authFeature = new AuthFeature(() => new CustomUserSession(),
			        new IAuthProvider[]
			        {
                        // We aren't creating new sessions so it's okay to create a private key (it won't be used)
				        new JwtAuthFirebaseProvider(appSettings),
				        new CredentialsAuthProvider(appSettings)     /* Sign In with Username / Password credentials */
				        //new FacebookAuthProvider(appSettings),        /* Create App https://developers.facebook.com/apps */
				        //new GoogleAuthProvider(appSettings),          /* Create App https://console.developers.google.com/apis/credentials */
				        //new MicrosoftGraphAuthProvider(appSettings),  /* Create App https://apps.dev.microsoft.com */
			        });
                

                appHost.Plugins.Add(authFeature);

                //appHost.Plugins.Add(new RegistrationFeature()); //Enable /register Service

                //override the default registration validation with your own custom implementation
                appHost.RegisterAs<CustomRegistrationValidator, IValidator<Register>>();
            });
    }
}
