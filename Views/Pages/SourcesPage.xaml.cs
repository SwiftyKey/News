using System.Windows.Controls;

namespace News.Views.Pages;

/**
	\brief Разделенный класс страницы отображения списка источников
	
	Наследуется от Page
*/
public partial class SourcesPage : Page
{
	/// Конструктор класса FeedsPage
	public SourcesPage()
	{
		InitializeComponent();
	}

	/**
		\brief Скрытый метод для регулировки прокрутки на странице
		\param[in] sender Элемент (SVScroll), у которого сгенерировалось событие
		\param[in] e Аргументы события прокрутки колесика мышки
	*/
	private void SVScroll_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScroll.ScrollToVerticalOffset(SVScroll.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}
}
