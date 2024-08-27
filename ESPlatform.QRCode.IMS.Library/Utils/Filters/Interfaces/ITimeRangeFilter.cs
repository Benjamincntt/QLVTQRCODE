namespace ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

public interface ITimeRangeFilter {
	public DateTime FromTime { get; set; }

	public DateTime ToTime { get; set; }
}
