namespace ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

public interface IDateRangeOptionalFilter {
	public DateTime? FromDate { get; set; }

	public DateTime? ToDate { get; set; }
}
