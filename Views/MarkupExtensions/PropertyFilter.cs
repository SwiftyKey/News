using System.Windows;

namespace News.Views.MarkupExtensions;

/**
	\brief Класс для регистрации свойств зависимостей

	Непосредственно отсеивает неподходящие объекты
	Наследуется от DependencyObject
*/
public class PropertyFilter : DependencyObject, IFilter
{
	/// Свойство зависимостей для PropertyName
	public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register
	(
		"PropertyName",
		typeof(string),
		typeof(PropertyFilter),
		new UIPropertyMetadata(null)
	);

	/// Имя свойства, которое сравнивается
	public string PropertyName
	{
		get => (string)GetValue(PropertyNameProperty);
		set => SetValue(PropertyNameProperty, value);
	}

	/// Свойство зависимостей для Value
	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register
	(
		"Value",
		typeof(object),
		typeof(PropertyFilter),
		new UIPropertyMetadata(null)
	);

	/// Значение с которым сравниваются другие объекты
	public object Value
	{
		get => GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	/**
		\brief Метод для фильтрации
		\param[in] item Значение свойства, которое сравнивается
		\return Булево значение, обозначающее, подходит ли свойство
	*/
	public bool Filter(object item)
	{
		var type = item.GetType();
		var itemValue = type.GetProperty(PropertyName).GetValue(item, null).ToString();
		return Equals(itemValue, Value);
	}
}