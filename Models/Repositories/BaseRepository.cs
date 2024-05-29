using Microsoft.EntityFrameworkCore;
using News.Models.Common;

namespace News.Models.Repositories;

/**
	\brief Абстрактный класс, для работы с базой данных
	
	Наследуется от IBaseRepository
*/
public abstract class BaseRepository<TEntity>(AppContext context) : IBaseRepository<TEntity> where TEntity : BaseEntity
{
	/// Текущий контекст данных
	readonly AppContext context = context;

	/// Текущая таблица
	protected DbSet<TEntity> set = null!;

	/**
		\brief Асинхронный метод, добавляющий сущность в базу данных
		\param[in] entity Добавляемый объект
		\return Добавленный объект типа BaseEntity
	*/
	public async Task<TEntity> AddAsync(TEntity entity)
	{
		return (await set.AddAsync(entity)).Entity;
	}

	/**
		\brief Асинхронный метод, добавляющий несколько сущностей в базу данных
		\param[in] entities Добавляемые объекты
	*/
	public async Task AddRangeAsync(IEnumerable<TEntity> entities)
	{
		await set.AddRangeAsync(entities);
	}

	/**
		\brief Метод, обновляющий указанную сущность в базе данных
		\param[in] entity Объект, который надо изменить
	*/
	public void Update(TEntity entity)
	{
		context.ChangeTracker.Clear();
		set.Update(entity);
	}

	/**
		\brief Метод, обновляющий указанные сущности в базе данных
		\param[in] entities Объекты, которые надо изменить
	*/
	public void UpdateRange(IEnumerable<TEntity> entities)
	{
		set.UpdateRange(entities);
	}

	/**
		\brief Метод, удаляющий указанную сущность из базы данных
		\param[in] entity Удаляемый объект
	*/
	public void Delete(TEntity entity)
	{
		var entityToDelete = set.First(e => e.Id == entity.Id);
		set.Remove(entityToDelete);
	}

	/**
		\brief Метод, удаляющий указанные сущности из базы данных
		\param[in] entity Удаляемые объекты
	*/
	public void DeleteRange(IEnumerable<TEntity> entities)
	{
		set.RemoveRange(entities);
	}

	public async Task SaveChangesAsync()
	{
		await context.SaveChangesAsync();
	}

	/**
		\brief Метод, возвращающий все объекты из таблицы базы данных
		\return Итерируемый объект, содержащий объекты типа BaseEntity
	*/
	public IEnumerable<TEntity> GetAll()
	{
		return set;
	}

	/**
		\brief Метод, возвращающий объект из таблицы по его ключу
		\param[in] id Первичный ключ требуемого объекта
		\return Объект типа BaseEntity
	*/
	public TEntity GetById(int id)
	{
		return set.First(el => el.Id == id);
	}
}
