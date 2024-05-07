namespace News.Models.Common;

public interface IReadRepository<TEntity> where TEntity : BaseEntity
{
	public IEnumerable<TEntity> GetAll();
	public TEntity GetById(int id);
}
