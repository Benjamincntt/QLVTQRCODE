using System.Globalization;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class NumberExtensions
{
    public static string FormatDecimal(this decimal value, int maxDecimalPlaces = 5)
    {
        // Tạo văn hóa Việt Nam
        var cultureInfo = new CultureInfo("vi-VN");

        // Chuyển đổi số thành chuỗi với tối đa `maxDecimalPlaces` chữ số thập phân
        string result = value.ToString($"N{maxDecimalPlaces}", cultureInfo);

        // Loại bỏ các chữ số 0 không cần thiết ở cuối phần thập phân
        if (result.Contains(','))
        {
            result = result.TrimEnd('0').TrimEnd(',');
        }

        return result;
    }
}