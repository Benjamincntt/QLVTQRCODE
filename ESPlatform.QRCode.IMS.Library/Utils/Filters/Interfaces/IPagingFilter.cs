namespace ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

public interface IPagingFilter {
	public int? PageIndex { get; set; }

	public int? PageSize { get; set; }
}
