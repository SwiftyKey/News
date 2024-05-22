using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Markup;

namespace News.Views.MarkupExtensions;

[ContentProperty("Filters")]
class FilterExtension : MarkupExtension
{
	private readonly Collection<IFilter> _filters = [];
	public Collection<IFilter> Filters { get { return _filters; } }

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