using ESPlatform.QRCode.IMS.Core.Engine.Configuration;

namespace ESPlatform.QRCode.IMS.Api.Extensions;

public static class RegisterImageRootPathServiceExtensions
{
    public static IServiceCollection RegisterImageRootPathService(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<ImagePath>(configuration.GetSection("ImagePath"));
        return services;
    }
}