using System.Linq.Expressions;
using ESPlatform.QRCode.IMS.Library.Utils.Mappers;
using Microsoft.EntityFrameworkCore;

namespace ESPlatform.QRCode.IMS.Library.Database.EfCore;

public class EfCoreRepositoryBase<T, TDbContext> : IRepositoryBase<T> where T : class where TDbContext : DbContext {
	// ReSharper disable once MemberCanBeProtected.Global
	public EfCoreRepositoryBase(TDbContext dbContext) {
		DbContext = dbContext;
	}

	protected TDbContext DbContext { get; set; }

	public async Task<int> InsertAsync(T t) {
		DbContext.Set<T>().Add(t);
		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> InsertManyAsync(IEnumerable<T> tList) {
		DbContext.Set<T>().AddRange(tList);
		return await DbContext.SaveChangesAsync();
	}

	public async Task<T?> GetAsync(params object[] keys) {
		return await DbContext.Set<T>().FindAsync(keys);
	}

	public async Task<IEnumerable<T>> ListAsync() {
		return await DbContext.Set<T>().ToListAsync();
	}

	public async Task<T?> GetAsync(Expression<Func<T, bool>> conditions) {
		return await DbContext.Set<T>().Where(conditions).FirstOrDefaultAsync();
	}

	public async Task<IEnumerable<T>> ListAsync(Expression<Func<T, bool>> conditions) {
		return await DbContext.Set<T>().Where(conditions).ToListAsync();
	}

	public async Task<bool> ExistsAsync(params object[] keys) {
		return await GetAsync(keys) != null;
	}

	public async Task<bool> ExistsAsync(Expression<Func<T, bool>> conditions) {
		return await DbContext.Set<T>().AnyAsync(conditions);
	}

	public async Task<int> CountAsync() {
		return await DbContext.Set<T>().CountAsync();
	}

	public async Task<int> CountAsync(Expression<Func<T, bool>> conditions) {
		return await DbContext.Set<T>().CountAsync(conditions);
	}

	public async Task<int> UpdateAsync(T t) {
		DbContext.ChangeTracker.Clear();
		DbContext.Set<T>().Update(t);

		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> UpdatePartialAsync(T t, params Expression<Func<T, object>>[] properties) {
		DbContext.ChangeTracker.Clear();
		DbContext.Set<T>().Attach(t);

		foreach (var prop in properties) {
			DbContext.Entry(t).Property(prop).IsModified = true;
		}

		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> UpdatePartialAsync(T t, object source) {
		DbContext.ChangeTracker.Clear();
		DbContext.Set<T>().Attach(t);

		var props = SimpleMapper.Map(source, t);
		foreach (var prop in props) {
			DbContext.Entry(t).Property(prop.Name).IsModified = true;
		}

		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> UpdateManyAsync(IEnumerable<T> tList) {
		DbContext.ChangeTracker.Clear();
		DbContext.Set<T>().UpdateRange(tList);

		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> UpdateManyPartialAsync(IEnumerable<T> tList, object[] sourceList) {
		DbContext.ChangeTracker.Clear();

        var entityType = DbContext.Model.FindEntityType(typeof(T)); 
        var keyNames = entityType?.FindPrimaryKey()?.Properties.Select(p => p.Name).ToHashSet() ?? new HashSet<string>();
		var i = 0;
		foreach (var t in tList) {
			if (i >= sourceList.Length) {
				break;
			}

			DbContext.Set<T>().Attach(t);

			var props = SimpleMapper.Map(sourceList[i], t);
			foreach (var prop in props) {
                // Kiểm tra xem thuộc tính có phải là khóa chính không
                if (!keyNames.Contains(prop.Name)) { 
                    DbContext.Entry(t).Property(prop.Name).IsModified = true;
                }
			}

			i++;
		}

		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> UpsertAsync(T t) {
		return await DbContext.Set<T>().Upsert(t).RunAsync();
	}

	public async Task<int> UpsertManyAsync(IEnumerable<T> tList) {
		return await DbContext.Set<T>().UpsertRange(tList).RunAsync();
	}

	public async Task<int> DeleteAsync(T t) {
		DbContext.Set<T>().Remove(t);
		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> DeleteManyAsync(Expression<Func<T, bool>> condition) {
		await DbContext.Set<T>().Where(condition).ExecuteDeleteAsync();
		return await DbContext.SaveChangesAsync();
	}

	public async Task<int> DeleteManyAsync(IEnumerable<T> tList) {
		DbContext.Set<T>().RemoveRange(tList);
		return await DbContext.SaveChangesAsync();
	}
}