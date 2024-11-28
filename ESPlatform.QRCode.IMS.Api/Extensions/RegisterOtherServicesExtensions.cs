using ESPlatform.QRCode.IMS.Library.Utils;
using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using MapsterMapper;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class RegisterOtherServicesExtensions {
	public static IServiceCollection RegisterOtherServices(this IServiceCollection services) {
		// Mapper config
		TypeAdapterConfig.GlobalSettings.Scan(AppDomain.CurrentDomain.GetAssemblies().Where(a => a.FullName?.StartsWith("ESPlatform") ?? false).ToArray());

		services.AddSingleton(TypeAdapterConfig.GlobalSettings)
				.AddScoped<IMapper, ServiceMapper>();

		// Validation config
		ValidatorOptions.Global.DisplayNameResolver = (type, member, _) => DisplayNameResolver.GetDisplayName(type, member);

		services.AddFluentValidationAutoValidation()
				.AddFluentValidationClientsideAdapters();

		// Caching config
		services.AddDistributedMemoryCache();

		return services;
	}
}
