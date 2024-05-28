using News.Models.Common;

namespace News.Models.Entities;

/**
	\brief Класс, предназначенный для создания источников
	
	Является сущностью в базе данных.
	Наследуется от BaseEntity
*/
public class Source : BaseEntity
{
	/// Название публикации
	string? url;
	/// Свойство для работы с url
	public string? Url
	{
		get => url;
		set
		{
			url = value;
			OnPropertyChanged(nameof(Url));
		}
	}

	/// Название источника
	public string? Title { get; set; }

	/// Описание источника
	public string? Description { get; set; }

	/// Ссылка на картинку источника
	public string? ImageUrl { get; set; }

	/// Все публикации данного источника
	public List<Feed> Feeds { get; set; } = [];
}
