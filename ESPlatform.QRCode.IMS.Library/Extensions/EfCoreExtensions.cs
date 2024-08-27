using ESPlatform.QRCode.IMS.Library.Utils.Filters;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Library.Extensions;

public static class EfCoreExtensions {
	public static async Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> query, int pageIndex, int pageSize) where T : class {
		var result = new PagedList<T> {
			PageIndex = pageIndex,
			PageSize = pageSize,
			Total = query.Count()
		};

		var pageCount = (double)result.Total / pageSize;
		result.PageCount = (int)Math.Ceiling(pageCount);

		var skip = (pageIndex - 1) * pageSize;
		result.Items = await query.Skip(skip).Take(pageSize).ToListAsync();
		return result;
	}
}
