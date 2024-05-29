using News.Models.Entities;
using News.Models.Repositories;

namespace News.ViewModels.Services;

/**
	\brief Класс, предназначенный для взаимодействия программы с репозиторием источников
	
	Работа с таблицей Sources происходит через данный сервис
*/
public class SourceService(SourceRepository sourceRepository)
{
	/**
		\brief Асинхронный метод, добавляющий источник в базу данных
		\param[in] source Добавляемый источник
	*/
	public async Task AddAsync(Source source)
	{
		await sourceRepository.AddAsync(source);
		await sourceRepository.SaveChangesAsync();
	}

	/**
		\brief Асинхронный метод, удаляющий указанный источник из базы данных
		\param[in] source Удаляемый источник
	*/
	public async Task DeleteAsync(Source source)
	{
		sourceRepository.Delete(source);
		await sourceRepository.SaveChangesAsync();
	}

	/**
		\brief Асинхронный метод, удаляющий несколько источников из базы данных
		\param[in] sources Коллекция источников
	*/
	public async Task DeleteRangeAsync(IEnumerable<Source> sources)
	{
		sourceRepository.DeleteRange(sources);
		await sourceRepository.SaveChangesAsync();
	}

	/**
		\brief Метод, возвращающий все источники из таблицы Sources
		\return Коллекция источников
	*/
	public IEnumerable<Source> GetAll()
	{
		return sourceRepository.GetAll();
	}

	/**
		\brief Метод, возвращающий источник по его первичному ключу
		\param[in] id Первичный ключ требуемого источника
		\return Полученный источник
	*/
	public Source GetById(int id)
	{
		return sourceRepository.GetById(id);
	}

	/**
		\brief Асинхронный метод, обновляющий указанный источник в базе данных
		\param[in] source Источник, который надо изменить
	*/
	public async Task UpdateAsync(Source source)
	{
		var entity = sourceRepository.GetById(source.Id);

		sourceRepository.Update(entity);
		await sourceRepository.SaveChangesAsync();
	}
}
