namespace News.Models.Common;

public interface IWriteRepository<TEntity> where TEntity : BaseEntity
{
	Task<TEntity> AddAsync(TEntity entity);
	Task AddRangeAsync(IEnumerable<TEntity> entities);
	void Update(TEntity entity);
	void UpdateRange(IEnumerable<TEntity> entities);
	void Delete(TEntity entity);
	void DeleteRange(IEnumerable<TEntity> entities);
	Task SaveChangesAsync();
}
