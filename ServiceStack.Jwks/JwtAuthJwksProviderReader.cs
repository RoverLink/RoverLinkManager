using ServiceStack.Auth;
using ServiceStack.Configuration;

namespace ServiceStack.Jwks;

public class JwtAuthJwksProviderReader : JwtAuthProviderReader
{
	private Dictionary<string,string> _publicKeyIds = new Dictionary<string,string>();

	public JwtAuthJwksProviderReader(IAppSettings appSettings) : base(appSettings)
	{

	}

	public override Task<object> AuthenticateAsync(IServiceBase authService, IAuthSession session, Authenticate request,
		CancellationToken token = new CancellationToken())
	{
		return base.AuthenticateAsync(authService, session, request, token);
	}
}