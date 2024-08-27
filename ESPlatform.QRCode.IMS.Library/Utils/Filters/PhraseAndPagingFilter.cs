using System.ComponentModel;
using ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

namespace ESPlatform.QRCode.IMS.Library.Utils.Filters;

public class PhraseAndPagingFilter : IPhraseFilter, IPagingFilter {
	[DisplayName("Từ khóa")]
	public string? Keywords { get; set; } = string.Empty;

	[DisplayName("Số trang")]
	public int? PageIndex { get; set; } = 1;

	[DisplayName("Dung lượng trang")]
	public int? PageSize { get; set; } = 20;
}
