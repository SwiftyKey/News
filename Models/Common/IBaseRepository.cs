namespace News.Models.Common;

/**
 \brief Интерфейс, который должны реализовать все репозитории

 Реализует интерфейсы IWriteRepository, IReadRepository
*/
public interface IBaseRepository<TEntity> :
	IWriteRepository<TEntity>, IReadRepository<TEntity> where TEntity : BaseEntity;
