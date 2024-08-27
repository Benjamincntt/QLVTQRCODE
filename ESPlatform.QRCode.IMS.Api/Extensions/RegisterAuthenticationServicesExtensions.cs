using System.Text;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Core.Facades.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class RegisterAuthenticationServicesExtensions {
	public static IServiceCollection RegisterAuthenticationServices(this IServiceCollection services) {
		services
			.AddAuthentication(options => {
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(options => {
				options.TokenValidationParameters = new TokenValidationParameters {
					ValidIssuer = AppConfig.Instance.JwtSettings.Issuer,
					ValidAudience = AppConfig.Instance.JwtSettings.Audience,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.Instance.JwtSettings.Key)),
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ClockSkew = TimeSpan.FromMinutes(1)
				};
				options.Events = new JwtBearerEvents {
					OnAuthenticationFailed = context => {
						if (context.Exception.GetType() == typeof(SecurityTokenExpiredException)) {
							context.Response.Headers.Add(Constants.Http.HeaderNames.TokenExpired, "true");
						}

						return Task.CompletedTask;
					}
				};
			});

		services
			.AddAuthorization();

		services
			.AddSingleton(AppConfig.Instance.JwtSettings)
			.AddSingleton(AppConfig.Instance.OtpSettings)
			.AddScoped<JwtFacade>()
			.AddScoped<OtpFacade>()
			;

		return services;
	}
}
