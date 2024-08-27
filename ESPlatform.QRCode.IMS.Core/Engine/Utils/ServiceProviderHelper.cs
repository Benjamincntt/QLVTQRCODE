using Microsoft.Extensions.DependencyInjection;

namespace ESPlatform.QRCode.IMS.Core.Engine.Utils;

public static class ServiceProviderHelper {
	public static IServiceProvider? ServiceProvider { get; set; }

	public static T? GetService<T>() {
		return ServiceProvider == null ? default : ServiceProvider.GetService<T>();
	}

	public static T? GetRequiredService<T>() where T : notnull {
		return ServiceProvider == null ? default : ServiceProvider.GetRequiredService<T>();
	}
}
