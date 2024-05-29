namespace News.Models.Common;

/// Интерфейс, определяющий базовую структуру репозиториев, связанную с чтением из базы данных
public interface IReadRepository<TEntity> where TEntity : BaseEntity
{
	/**
		\brief Метод, возвращающий все объекты из таблицы базы данных
		\return Итерируемый объект, содержащий объекты типа BaseEntity
	*/
	public IEnumerable<TEntity> GetAll();

	/**
		\brief Метод, возвращающий объект из таблицы по его ключу
		\param[in] id Первичный ключ требуемого объекта
		\return Объект типа BaseEntity
	*/
	public TEntity GetById(int id);
}
