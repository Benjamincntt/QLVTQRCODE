using System.Reflection;
using System.Text.Json.Serialization;
using ESPlatform.QRCode.IMS.Api.Filters;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Configuration;
using ESPlatform.QRCode.IMS.Library.Utils.Converters;
using Microsoft.OpenApi.Models;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class ConfigureServicesExtensions {
	public static IServiceCollection ConfigureControllers(this IServiceCollection services) {
		services
		   .AddControllers(options => options.Filters.Add<ApiParametersValidationFilter>())
		   .AddJsonOptions(options => {
				options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
				options.JsonSerializerOptions.Converters.Add(new JsonNullableConverterFactory());
#pragma warning disable SYSLIB0020
				options.JsonSerializerOptions.IgnoreNullValues = true;
#pragma warning disable SYSLIB0020
			})
		   .ConfigureApiBehaviorOptions(x => x.SuppressModelStateInvalidFilter = true);

		return services;
	}

	public static IServiceCollection ConfigureSwaggerOptions(this IServiceCollection services) {
		services
		   .AddEndpointsApiExplorer()
		   .AddSwaggerGen(x => {
				x.DescribeAllParametersInCamelCase();
				x.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
				x.EnableAnnotations();
				x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer"
				});

				x.AddSecurityRequirement(new OpenApiSecurityRequirement()
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							},
							Scheme = "oauth2",
							Name = "Bearer",
							In = ParameterLocation.Header,
						},
						new List<string>()
					}
				});
			});

		return services;
	}

	public static IServiceCollection ConfigureNetworkOptions(this IServiceCollection services) {
		services
		   .AddHttpContextAccessor()
		   .AddRouting(options => {
				options.LowercaseUrls = true;
				options.LowercaseQueryStrings = true;
			})
		   .AddCors(options => {
				options.AddPolicy(
					Constants.Http.CorsPolicyName,
					corsPolicyBuilder => corsPolicyBuilder
										.WithOrigins(AppConfig.Instance.AllowedDomains.ToArray())
										.SetIsOriginAllowedToAllowWildcardSubdomains()
										.SetIsOriginAllowed(_ => true)
										.AllowAnyHeader()
										.AllowAnyMethod()
										.AllowCredentials()
				);
			});

		return services;
	}
}
