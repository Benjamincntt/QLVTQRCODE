using System.Linq.Expressions;

namespace ESPlatform.QRCode.IMS.Library.Database;

public interface IRepositoryBase<T> where T : class {
	Task<int> InsertAsync(T t);

	Task<int> InsertManyAsync(IEnumerable<T> tList);

	Task<T?> GetAsync(params object[] keys);

	Task<T?> GetAsync(Expression<Func<T, bool>> conditions);

	Task<IEnumerable<T>> ListAsync();

	Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> conditions);

	Task<bool> ExistsAsync(params object[] keys);

	Task<bool> ExistsAsync(Expression<Func<T, bool>> conditions);

	Task<int> CountAsync();

	Task<int> CountAsync(Expression<Func<T, bool>> conditions);

	Task<int> UpdateAsync(T t);

	Task<int> UpdatePartialAsync(T t, params Expression<Func<T, object>>[] properties);

	Task<int> UpdatePartialAsync(T t, object source);

	Task<int> UpdateManyAsync(IEnumerable<T> tList);

	Task<int> UpdateManyPartialAsync(IEnumerable<T> tList, object[] sourceList);

	Task<int> UpsertAsync(T t);

	Task<int> UpsertManyAsync(IEnumerable<T> tList);

	Task<int> DeleteAsync(T t);

	Task<int> DeleteManyAsync(IEnumerable<T> tList);

	Task<int> DeleteManyAsync(Expression<Func<T, bool>> condition);
}