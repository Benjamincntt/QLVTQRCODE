using ESPlatform.QRCode.IMS.Library.Exceptions;
using ESPlatform.QRCode.IMS.Library.Extensions;
using ESPlatform.QRCode.IMS.Core.Engine;
using ESPlatform.QRCode.IMS.Core.Engine.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;

namespace ESPlatform.QRCode.IMS.Api.Middlewares;

public class AuthenticationMiddleware {
    private readonly RequestDelegate _next;

    public AuthenticationMiddleware(RequestDelegate next) {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IDistributedCache distributedCache) {
        var requestPath = context.Request.Path.Value;

        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".webp", ".svg" };
        var pdfExtension = ".pdf";

        if (requestPath != null) {
            if (imageExtensions.Any(ext => requestPath.EndsWith(ext, StringComparison.OrdinalIgnoreCase))) {
                await _next(context);
                return;
            }

            if (requestPath.EndsWith(pdfExtension, StringComparison.OrdinalIgnoreCase)) {
                context.Response.Headers["Content-Type"] = "application/pdf";
                context.Response.Headers["Content-Disposition"] = "inline";
                await _next(context);
                return;
            }
        }

        var currentKyKiemKeId = context.GetKyKiemKeId();
        var endpoint = context.GetEndpoint();

        if (endpoint?.Metadata.GetMetadata<IAllowAnonymous>() == null
            && context.User.Identity?.IsAuthenticated != true) {
            throw new BadRequestException(Constants.Exceptions.Messages.Common.Unauthorized);
        }

        var currentAccountId = context.GetAccountId();

        context.Items[Constants.ContextKeys.KyKiemKeId] = currentKyKiemKeId;
        context.Items[Constants.ContextKeys.IpAddress] = context.GetClientIpAddress();
        context.Items[Constants.ContextKeys.AccountId] = currentAccountId;
        context.Items[Constants.ContextKeys.Username] = context.GetUsername();
        await _next(context);
    }
}
