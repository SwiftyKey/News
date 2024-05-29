namespace News.Models.Common;

/// Интерфейс, определяющий базовую структуру репозиториев, связанную с записью в базу данных
public interface IWriteRepository<TEntity> where TEntity : BaseEntity
{
	/**
		\brief Асинхронный метод, добавляющий сущность в базу данных
		\param[in] entity Добавляемый объект
		\return Добавленный объект типа BaseEntity
	*/
	Task<TEntity> AddAsync(TEntity entity);

	/**
		\brief Асинхронный метод, добавляющий несколько сущностей в базу данных
		\param[in] entities Добавляемые объекты
	*/
	Task AddRangeAsync(IEnumerable<TEntity> entities);

	/**
		\brief Метод, обнавляющий указанную сущность в базе данных
		\param[in] entity Объект, который надо изменить
	*/
	void Update(TEntity entity);

	/**
		\brief Метод, обнавляющий указанные сущности в базе данных
		\param[in] entities Объекты, которые надо изменить
	*/
	void UpdateRange(IEnumerable<TEntity> entities);

	/**
		\brief Метод, удаляющий указанную сущность в базе данных
		\param[in] entity Удаляемый объект
	*/
	void Delete(TEntity entity);

	/**
		\brief Метод, удаляющий указанные сущности в базе данных
		\param[in] entity Удаляемые объекты
	*/
	void DeleteRange(IEnumerable<TEntity> entities);

	/// Асинхронный метод, сохраняющий изменения внесенные в базу данных
	Task SaveChangesAsync();
}
