using News.Models.Common;

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

	/// Булево значение, показывающее, добавлена ли данная публикация в избранное
	bool isFavourite = false;
	/// Свойство для работы с isFavourite
	public bool IsFavourite
	{
		get => isFavourite;
		set
		{
			isFavourite = value;
			OnPropertyChanged(nameof(isFavourite));
		}
	}

	/// Булево значение, показывающее, добавлена ли данная публикация в отложенное
	bool isReadLater = false;
	/// Свойство для работы с isReadLater
	public bool IsReadLater
	{
		get => isReadLater;
		set
		{
			isReadLater = value;
			OnPropertyChanged(nameof(isReadLater));
		}
	}

	/// Источник данной публикации
	public Source Source { get; set; } = null!;
	/// Внешний ключ источника
	public int SourceId { get; set; }
}
