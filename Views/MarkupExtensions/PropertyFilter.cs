using System.Windows;

namespace News.Views.MarkupExtensions;

public class PropertyFilter : DependencyObject, IFilter
{
	public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register
	(
		"PropertyName",
		typeof(string),
		typeof(PropertyFilter),
		new UIPropertyMetadata(null)
	);

	public string PropertyName
	{
		get => (string)GetValue(PropertyNameProperty);
		set => SetValue(PropertyNameProperty, value);
	}

	public static readonly DependencyProperty ValueProperty = DependencyProperty.Register
	(
		"Value",
		typeof(object),
		typeof(PropertyFilter),
		new UIPropertyMetadata(null)
	);

	public object Value
	{
		get => GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public bool Filter(object item)
	{
		var type = item.GetType();
		var itemValue = type.GetProperty(PropertyName).GetValue(item, null).ToString();
		return Equals(itemValue, Value);
	}
}