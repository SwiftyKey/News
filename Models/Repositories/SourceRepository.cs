using News.Models.Entities;

namespace News.Models.Repositories;

/**
	\brief Класс, для работы с таблицой Sources
	
	Наследуется от BaseRepository
*/
public class SourceRepository : BaseRepository<Source>
{
	/**
		\brief Конструктор класса SourceRepository
		\param[in] appContext контекст базы данных
	*/
	public SourceRepository(AppContext appContext) : base(appContext)
	{
		set = appContext.Sources;
	}
}
