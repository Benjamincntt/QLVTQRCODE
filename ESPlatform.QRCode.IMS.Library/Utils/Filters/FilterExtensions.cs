using ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

namespace ESPlatform.QRCode.IMS.Library.Utils.Filters;

public static class FilterExtensions {
	public static int GetPageIndex(this IPagingFilter pagingFilter, int defaultValue = 1) {
		return pagingFilter.PageIndex ?? defaultValue;
	}

	public static int GetPageSize(this IPagingFilter pagingFilter, int defaultValue = 20) {
		return pagingFilter.PageSize ?? defaultValue;
	}
}
