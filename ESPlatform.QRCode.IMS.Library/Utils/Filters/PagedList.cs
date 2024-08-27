using ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

namespace ESPlatform.QRCode.IMS.Library.Utils.Filters;

public class PagedList<T> : IPagedList where T : class {
	public int PageIndex { get; set; }

	public int PageCount { get; set; }

	public int PageSize { get; set; }

	public int Total { get; set; }

	public int FirstRowOnPageIndex => (PageIndex - 1) * PageSize + 1;

	public int LastRowOnPageIndex => Math.Min(PageIndex * PageSize, Total);

	public IEnumerable<T> Items { get; set; } = new List<T>();
}
