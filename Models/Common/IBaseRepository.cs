namespace News.Models.Common;

public interface IBaseRepository<TEntity> :
	IWriteRepository<TEntity>, IReadRepository<TEntity> where TEntity : BaseEntity;
