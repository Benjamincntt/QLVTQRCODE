using System.ComponentModel;
using ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

namespace ESPlatform.QRCode.IMS.Library.Utils.Filters;

public class PagingFilter : IPagingFilter {
	[DisplayName("Số trang")]
	public int? PageIndex { get; set; } = 1;

	[DisplayName("Dung lượng mỗi trang")]
	public int? PageSize { get; set; } = 20;
}
