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

        // Kiểm tra nếu đường dẫn yêu cầu bắt đầu bằng /images
        if (requestPath.StartsWith("/images", StringComparison.OrdinalIgnoreCase))
        {
            // Loại bỏ phần /images từ đường dẫn yêu cầu
            var relativePath = requestPath.Substring(8).TrimStart('/');

            // Xác định đường dẫn đầy đủ đến tệp
            var filePath = Path.Combine(@"D:\Images", relativePath);

            // Kiểm tra nếu tệp tồn tại
            if (File.Exists(filePath))
            {
                // Đặt kiểu nội dung dựa trên phần mở rộng của tệp
                var contentType = "application/octet-stream";
                var extension = Path.GetExtension(filePath).ToLowerInvariant();
                if (extension == ".jpg" || extension == ".jpeg")
                {
                    contentType = "image/jpeg";
                }
                else if (extension == ".png")
                {
                    contentType = "image/png";
                }
                else if (extension == ".gif")
                {
                    contentType = "image/gif";
                }

                context.Response.ContentType = contentType;
                await context.Response.SendFileAsync(filePath);
                return;
            }
        }

        // Chuyển tiếp yêu cầu cho các middleware khác nếu tệp không tồn tại
        await _next(context);
    }
}