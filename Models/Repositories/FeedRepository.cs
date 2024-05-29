using News.Models.Entities;

namespace News.Models.Repositories;

/**
	\brief Класс, для работы с таблицой Feeds
	
	Наследуется от BaseRepository
*/
public class FeedRepository : BaseRepository<Feed>
{
	/**
		\brief Конструктор класса FeedRepository
		\param[in] appContext контекст базы данных
	*/
	public FeedRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Feeds;
	}

	/**
		\brief Метод, получения публикации по ее ссылке
		\param[in] url ссылка на публикацию
		\return Полученная публикация
	*/
	public Feed? GetByUrl(string url) => set.FirstOrDefault(x => x.Link == url);
}
