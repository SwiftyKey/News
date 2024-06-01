using News.Models.Entities;

namespace News.Models.Repositories;

/**
	\brief Класс, для работы с таблицой Publications
	
	Наследуется от BaseRepository
*/
public class PublicationRepository : BaseRepository<Publication>
{
	/**
		\brief Конструктор класса PublicationRepository
		\param[in] appContext контекст базы данных
	*/
	public PublicationRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Publications;
	}

	/**
		\brief Метод, получения публикации по ее ссылке
		\param[in] url ссылка на публикацию
		\return Полученная публикация
	*/
	public Publication? GetByUrl(string url) => set.FirstOrDefault(x => x.Link == url);
}
