namespace ESPlatform.QRCode.IMS.Library.Utils.Filters.Interfaces;

public interface IPagedList {
	public int PageIndex { get; set; }

	public int PageCount { get; set; }

	public int PageSize { get; set; }

	public int Total { get; set; }

	public int FirstRowOnPageIndex => (PageIndex - 1) * PageSize + 1;

	public int LastRowOnPageIndex => Math.Min(PageIndex * PageSize, Total);
}
