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

                // This is the full key (\n converted to newline) from google at https://www.googleapis.com/robot/v1/metadata/x509/securetoken@system.gserviceaccount.com in Base64 format
                //var pemKey = "LS0tLS1CRUdJTiBDRVJUSUZJQ0FURS0tLS0tCk1JSURIVENDQWdXZ0F3SUJBZ0lKQUwvLzBRc1VMMUgxTUEwR0NTcUdTSWIzRFFFQkJRVUFNREV4THpBdEJnTlYKQkFNTUpuTmxZM1Z5WlhSdmEyVnVMbk41YzNSbGJTNW5jMlZ5ZG1salpXRmpZMjkxYm5RdVkyOXRNQjRYRFRJeQpNVEl4TXpBNU16a3hNVm9YRFRJeU1USXlPVEl4TlRReE1Wb3dNVEV2TUMwR0ExVUVBd3dtYzJWamRYSmxkRzlyClpXNHVjM2x6ZEdWdExtZHpaWEoyYVdObFlXTmpiM1Z1ZEM1amIyMHdnZ0VpTUEwR0NTcUdTSWIzRFFFQkFRVUEKQTRJQkR3QXdnZ0VLQW9JQkFRQzVtR2dnUCtsZ3NvQnc3OHF4TjNBeVlrd1ZxZS8rWDlyN090Q0pKK1JFQjkrcQpuY0ZJVWhjNlg3ZDlhaHpFSDFDNHhlRkRzU0FNMnJlVHV3RWFWQnVIQWY0ZkxGVm12TTV3dTMrVWRXdWQ4bnFNCm80amM0YWlZSTJkbzV4dGNBdWdyWHM0azdFbktmWFQwaTB0RUpmSjlXRkFIRHJUTkFwWW5VMDJiT0pQbHpVU0EKVlNCZUZzRVIrbFhrcGJmWDRlTkhpUzBNQjNJQ3AwVExNcW1FVm9qZzdHSVZ1cEtwcWFRK0dPZmVwdUFZZzdjQwpKQ0xKd2MxZ0oyMGtWcVFVOVFGcGxHSTBwVzNZWjYzK29IclJodXY3aWJkR3MyYXM3VERTTTcvbUxKNGc0bjNyCkRVT2ZLVzBWOVNyVW5iY09CRDdmRlNCL0Fjd0QyYktKUEt0bW1SVlJBZ01CQUFHak9EQTJNQXdHQTFVZEV3RUIKL3dRQ01BQXdEZ1lEVlIwUEFRSC9CQVFEQWdlQU1CWUdBMVVkSlFFQi93UU1NQW9HQ0NzR0FRVUZCd01DTUEwRwpDU3FHU0liM0RRRUJCUVVBQTRJQkFRQktIaFN5c2hJeEtRNkc4YzRSRXU0REMzaVNnVldBZlppKzRITTErWHhXClRDZTdsRU82NHNRMXp5dHovNXdBdDhHdmZtc0xZYXl5Wk1IbURydHMrUDBncTRqWVpNZm15NlZQdWkvbE80RDYKVitvazl4d3QyOThJakE0S0Zyd3lnbW9XYlViclM3VnUvcmY1L3drRmJxbzBTcGpMTTV2cUtqQit5UGROdXNzcwpVRmlVVkE0VGdHeW1neEFtenlOVW5lMHJFMDJub3YvcTgremdKUGJKUnlveVpaN2VsZU14MFdwTFpndWttWlVMClkvVVpQTUl3QXFkdGZGTDdBQnE0NDI1akpYTGE0YjRFZi8wWnZLaVdOek5LM0NRcER2VlhKTFFJeXQ4WWQyZXAKdmVUUFBQUk5WalhIQi9mazVBSjBDM2F3VGZydmFGcGYyMkZRVUdJNFpLWFYKLS0tLS1FTkQgQ0VSVElGSUNBVEUtLS0tLQ==";
                //var cert = new X509Certificate2(Convert.FromBase64String(pemKey));
                //var pkey = cert.PublicKey.GetRSAPublicKey();
		        //var pkeyRsaParameters = pkey.ExportParameters(false);

		        var authFeature = new AuthFeature(() => new CustomUserSession(),
			        new IAuthProvider[]
			        {
                        // We aren't creating new sessions so it's okay to create a private key (it won't be used)
				        new JwtAuthFirebaseProvider(appSettings)
				        //,
				        //new CredentialsAuthProvider(appSettings),     /* Sign In with Username / Password credentials */
				        //new FacebookAuthProvider(appSettings),        /* Create App https://developers.facebook.com/apps */
				        //new GoogleAuthProvider(appSettings),          /* Create App https://console.developers.google.com/apis/credentials */
				        //new MicrosoftGraphAuthProvider(appSettings),  /* Create App https://apps.dev.microsoft.com */
			        });
                
                /*
                authFeature.RegisterPlugins.Add(new JwksFeature()
                {
                    OpenIdDiscoveryUrl = $"https://securetoken.google.com/{firebaseId}/.well-known/openid-configuration"
                });
                */

                appHost.Plugins.Add(authFeature);

                appHost.Plugins.Add(new RegistrationFeature()); //Enable /register Service

                //override the default registration validation with your own custom implementation
                appHost.RegisterAs<CustomRegistrationValidator, IValidator<Register>>();
            });
    }
}
