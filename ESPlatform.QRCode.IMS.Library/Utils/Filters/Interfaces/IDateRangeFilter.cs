namespace ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

public interface IDateRangeFilter {
	public DateTime FromDate { get; set; }

	public DateTime ToDate { get; set; }
}
