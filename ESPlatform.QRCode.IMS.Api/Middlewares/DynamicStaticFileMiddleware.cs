using ESPlatform.QRCode.IMS.Core.Engine.Configuration;

namespace ESPlatform.QRCode.IMS.Api.Middlewares;

public class DynamicStaticFileMiddleware
{
    private readonly RequestDelegate _next;

    public DynamicStaticFileMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Xác định đường dẫn yêu cầu
        var requestPath = context.Request.Path.Value;
        var imageFolderPath = AppConfig.Instance.ImagePath.RootPath;
        var imageUrlPath = AppConfig.Instance.ImagePath.RelativeBasePath; 
        var pdfFolderPath = AppConfig.Instance.KySoPathVersion2.RootPath;
        var pdfUrlPath = AppConfig.Instance.KySoPathVersion2.RelativeBasePath;
        
        // Kiểm tra nếu đường dẫn yêu cầu bắt đầu bằng imageUrlPath
        if (requestPath.StartsWith(imageUrlPath, StringComparison.OrdinalIgnoreCase))
        {
            var relativePath = requestPath.Substring(imageUrlPath.Length).Replace("/", "\\");
            var localBasePath =  (imageFolderPath + imageUrlPath).Replace("/", "\\");
            // Xác định đường dẫn đầy đủ đến tệp
            var filePath = localBasePath + relativePath;

            // Kiểm tra nếu tệp tồn tại
            if (File.Exists(filePath))
            {
                // Đặt kiểu nội dung dựa trên phần mở rộng của tệp
                var contentType = GetContentType(filePath);
                context.Response.ContentType = contentType;
                await context.Response.SendFileAsync(filePath);
                return;
            }
        }
        else if (requestPath.StartsWith(pdfUrlPath, StringComparison.OrdinalIgnoreCase))
        {
            var relativePath = requestPath.Substring(pdfUrlPath.Length).Replace("/", "\\");
            var localBasePath =  (pdfFolderPath + pdfUrlPath).Replace("/", "\\");
            // Xác định đường dẫn đầy đủ đến tệp
            var filePath = localBasePath + relativePath;

            // Kiểm tra nếu tệp tồn tại
            if (File.Exists(filePath))
            {
                // Đặt kiểu nội dung dựa trên phần mở rộng của tệp
                var contentType = GetContentType(filePath);
                context.Response.ContentType = contentType;
                await context.Response.SendFileAsync(filePath);
                return;
            }
        }

        // Chuyển tiếp yêu cầu cho các middleware khác nếu tệp không tồn tại
        await _next(context);
    }
    private string GetContentType(string filePath)
    {
        var extension = Path.GetExtension(filePath).ToLowerInvariant();
        return extension switch
        {
            ".jpg" or ".jpeg" => "image/jpeg",
            ".png" => "image/png",
            ".gif" => "image/gif",
            ".pdf" => "application/pdf",
            _ => "application/octet-stream",
        };
    }
}