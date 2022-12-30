using System.Collections;
using ServiceStack.Auth;
using ServiceStack.Configuration;
using System.Security.Cryptography;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using ServiceStack.Host;
using ServiceStack.Text;

namespace ServiceStack.Jwks;

public class JwtAuthFirebaseProvider : JwtAuthProviderReader
{
	private Dictionary<string, JsonWebKey> _publicKeyIds = new Dictionary<string,JsonWebKey>();
	public string OpenIdDiscoveryUrl { get; set; }
	public string JwksUrl { get; set; }
	private string _firebaseProjectId { get; set; }

	public IRestClient JwksClient { get; set; } = new JsonServiceClient();

	public JwtAuthFirebaseProvider(IAppSettings appSettings, string firebaseProjectId) : base(appSettings)
	{
		_firebaseProjectId = firebaseProjectId;

		Audience = _firebaseProjectId;
		OpenIdDiscoveryUrl = $"https://securetoken.google.com/{_firebaseProjectId}/.well-known/openid-configuration";
		HashAlgorithm = "RS256";
		PrivateKey = RsaUtils.CreatePrivateKeyParams(RsaKeyLengths.Bit2048);
		UseTokenCookie = false;
		ValidateToken = (JsonObject json, ServiceStack.Web.IRequest request) =>
		{
			var token = request.GetJwtToken();
			var jwt = new JwtSecurityToken(token);

			// Validate audience matches
			if (!jwt.Audiences.Contains(Audience))
				return false;

			// Validate expiration
			if (jwt.ValidTo < DateTime.UtcNow)
				return false;
			
			// Check to see if we have a public key in hand that can decode this token
			if (!_publicKeyIds.ContainsKey(jwt.Header.Kid))
			{
				var keySet = RetrieveKeySet();
				LoadKeySet(keySet);
			}
			
			return true;
		};
	}

	public JwtAuthFirebaseProvider Initialize()
    {
		RetrieveOpenIdDiscovery();

	    var keySet = RetrieveKeySet();
	    LoadKeySet(keySet);

	    return this;
    }

	public override Task<object> AuthenticateAsync(IServiceBase authService, IAuthSession session, Authenticate request,
		CancellationToken token = new CancellationToken())
	{
		return base.AuthenticateAsync(authService, session, request, token);
	}

	protected virtual void RetrieveOpenIdDiscovery()
	{
		if (!string.IsNullOrEmpty(OpenIdDiscoveryUrl))
		{
			var discoveryDoc = JwksClient.Get<OpenIdDiscoveryDocument>(OpenIdDiscoveryUrl);
			Issuer = discoveryDoc.Issuer;
			JwksUrl = discoveryDoc.JwksUri;
		}
	}

	protected virtual JsonWebKeySetResponse RetrieveKeySet()
	{
		if (string.IsNullOrEmpty(JwksUrl))
		{
			Log.Error($"Missing {nameof(JwksUrl)} {JwksUrl}");

			return new();
		}

		return JwksClient.Get<JsonWebKeySetResponse>(JwksUrl);
	}

	protected virtual void LoadKeySet(JsonWebKeySetResponse keySet)
	{
		if (keySet?.Keys.IsEmpty() ?? true)
		{
			Log.Error($"Unable to load KeySet in JwtAuthFirebaseProvider - Expecting at least one key from keyset {keySet.Dump()}");

			return;
		}

		if (RequireHashAlgorithm)
		{
			// infer the algorithm if it is described by a key from the set
			var algorithm = keySet.Keys.FirstOrDefault(x => !string.IsNullOrEmpty(x.Algorithm))?.Algorithm;
			if (algorithm != null)
			{
				HashAlgorithm = algorithm;
			}
		}

		var key = keySet.Keys.First();
		
		PublicKey = ToRsaParameters(key);
		KeyId = key.KeyId;

        try
        {
	        FallbackPublicKeys = keySet.Keys.Skip(1)
		        // ReSharper disable once PossibleInvalidOperationException
		        .Select(x => ToRsaParameters(x).Value)
		        .ToList();
        }
		catch (InvalidOperationException e)
        {
			Log.Error("Unable to load FallbackPublicKeys due to RSA parameter conversion error", e);
        }

		_publicKeyIds = keySet.Keys.ToDictionary(x => x.KeyId, x => x);
	}

	static RSAParameters? ToRsaParameters(JsonWebKey key)
	{
		if (key == null) return null;

		return new RSAParameters
		{
			Exponent = key.Exponent.FromBase64UrlSafe(),
			Modulus = key.Modulus.FromBase64UrlSafe()
		};
	}
}