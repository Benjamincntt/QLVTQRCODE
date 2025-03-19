using ESPlatform.QRCode.IMS.Core.Engine.Configuration;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class RegisterKySoRootPathServicesExtensions
{
    public static IServiceCollection RegisterKySoRootPathService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<KySoPathVersion2>(configuration.GetSection("KySoPathVersion2"));
        return services;
    }
}