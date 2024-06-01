using News.Models.Common;
using System.Collections.ObjectModel;

/**
	\brief Пространство имен, в котором содержатся классы сущностей базы данных
	\param Содержит классы сущностей:
		@ref Publication
		@ref Source
*/
namespace News.Models.Entities;

/**
	\brief Класс, предназначенный для создания онлайн-публикаций
	
	Является сущностью в базе данных.
	Наследуется от BaseEntity
*/
public class Publication : BaseEntity
{
	/// Название публикации
	public required string Title { get; set; }
	/// Ссылка на публикаию в интернете
	public required string Link { get; set; }
	/// Дата выхода публикации
	public DateTime? PublishingDate { get; set; }

	public ObservableCollection<User> UserFavourites { get; set; } = [];
	public ObservableCollection<Favourite> Favourites { get; set; } = [];

	public ObservableCollection<User> UserReadLater { get; set; } = [];
	public ObservableCollection<ReadLater> ReadLater { get; set; } = [];
	/// Источник данной публикации
	public Source Source { get; set; } = null!;
	/// Внешний ключ источника
	public int SourceId { get; set; }
}
