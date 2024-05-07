using Microsoft.EntityFrameworkCore;
using News.Models.Common;

namespace News.Models.Repositories;

public abstract class BaseRepository<TEntity>(AppContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
	readonly AppContext context = context;
	protected DbSet<TEntity> set = null!;

	public async Task<TEntity> AddAsync(TEntity entity)
	{
		return (await set.AddAsync(entity)).Entity;
	}

	public async Task AddRangeAsync(IEnumerable<TEntity> entities)
	{
		await set.AddRangeAsync(entities);
	}

	public void Update(TEntity entity)
	{
		context.ChangeTracker.Clear();
		set.Update(entity);
	}

	public void UpdateRange(IEnumerable<TEntity> entities)
	{
		set.UpdateRange(entities);
	}

	public void Delete(TEntity entity)
	{
		var entityToDelete = set.First(e => e.Id == entity.Id);
		set.Remove(entityToDelete);
	}

	public void DeleteRange(IEnumerable<TEntity> entities)
	{
		set.RemoveRange(entities);
	}

	public async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}

	public IEnumerable<TEntity> GetAll()
	{
		return set;
	}

	public TEntity GetById(int id)
	{
		return set.First(el => el.Id == id);
	}
}
