using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Markup;

/**
	\brief Пространство имен, в котором содержатся классы расширения разметки
	\param Содержит классы:
		@ref FilterExtension
		@ref IFilter
		@ref PropertyFilter
*/
namespace News.Views.MarkupExtensions;

[ContentProperty("Filters")]
/**
	\brief Класс расширения разметки

	Предназначен для фильтрации публикаций в списках отложенное и избранное
	Наследуется от MarkupExtension
*/
class FilterExtension : MarkupExtension
{
	private readonly Collection<IFilter> _filters = [];
	/// Коллекция фильтров
	public Collection<IFilter> Filters { get { return _filters; } }

	/**
		\brief Переопределенный метод, применяющий фильтры к данным
		\param[in] serviceProvider Объект, обеспечивающий настраиваемую поддержку для других объектов
		\return Обработчик события фильтра
	*/
	public override object ProvideValue(IServiceProvider serviceProvider)
	{
		return new FilterEventHandler((s, e) =>
		{
			foreach (var filter in Filters)
			{
				var res = filter.Filter(e.Item);
				if (!res)
				{
					e.Accepted = false;
					return;
				}
			}
			e.Accepted = true;
		});
	}
}