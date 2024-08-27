using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace ESPlatform.QRCode.IMS.Core.Facades.Authentication;

public class JwtFacade {
	private const string SigningAlgorithm = SecurityAlgorithms.HmacSha512Signature;
	private const string ValidationAlgorithm = SecurityAlgorithms.HmacSha512;

	private readonly JwtSettings _jwtSettings;

	public JwtFacade(JwtSettings jwtSettings) {
		_jwtSettings = jwtSettings;
	}

	public string GenerateAccessToken(int accountId, string username, IDictionary<string, string>? otherClaims = null) {
		var keyBytes = Encoding.ASCII.GetBytes(_jwtSettings.Key);

		var claims = new List<Claim> {
			new(ClaimTypes.NameIdentifier, accountId.ToString()),
			new(ClaimTypes.Name, username)
		};
		if (otherClaims != null) {
			claims.AddRange(otherClaims.Select(otherClaim => new Claim(otherClaim.Key, otherClaim.Value)));
		}

		var tokenHandler = new JwtSecurityTokenHandler();
		var token = tokenHandler.CreateToken(new SecurityTokenDescriptor {
			Issuer = _jwtSettings.Issuer,
			Audience = _jwtSettings.Audience,
			Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.AccessTokenLifetimeMinutes),
			NotBefore = DateTime.UtcNow,
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SigningAlgorithm),
			Subject = new ClaimsIdentity(claims)
		});
		return tokenHandler.WriteToken(token);
	}

	public ClaimsPrincipal? ValidateAccessToken(string accessToken, bool validateLifetime = true) {
		if (string.IsNullOrEmpty(accessToken)) {
			return null;
		}

		var tokenHandler = new JwtSecurityTokenHandler();
		var keyBytes = Encoding.ASCII.GetBytes(_jwtSettings.Key);
		try {
			var principal = tokenHandler.ValidateToken(accessToken, new TokenValidationParameters {
				ValidateIssuer = false,
				ValidateAudience = false,
				ValidateLifetime = validateLifetime,
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
				ClockSkew = TimeSpan.FromMinutes(1)
			}, out var validatedToken);

			if (validatedToken is not JwtSecurityToken jwtValidatedToken
			 || !jwtValidatedToken.Header.Alg.Equals(ValidationAlgorithm, StringComparison.InvariantCultureIgnoreCase)) {
				return null;
			}

			return principal;
		}
		catch {
			return null;
		}
	}
}
