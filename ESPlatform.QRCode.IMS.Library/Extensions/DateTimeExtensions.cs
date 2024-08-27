namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class DateTimeExtensions {
	public static DateTime StartOfDay(this DateTime dt) {
		return dt.Date;
	}

	public static DateTime EndOfDay(this DateTime dt) {
		return dt.Date.AddDays(1).AddTicks(-1);
	}
}
