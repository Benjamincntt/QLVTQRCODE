using Serilog;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class RegisterSerilogExtensions {
	public static WebApplicationBuilder RegisterSerilog(this WebApplicationBuilder builder) {
		builder.Host
			   .UseSerilog((context, services, config) => config
														  .ReadFrom.Configuration(context.Configuration)
														  .ReadFrom.Services(services)
														  .Enrich.FromLogContext()
			   );

		return builder;
	}
}
