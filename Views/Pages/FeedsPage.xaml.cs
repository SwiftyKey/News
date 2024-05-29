using System.Windows.Controls;

/**
	\brief Пространство имен, в котором содержатся страницы главного окна приложения
	\param Содержит классы:
		@ref FeedsPage
		@ref SettingsPage
		@ref SourcesPage
*/
namespace News.Views.Pages;

/**
	\brief Разделенный класс страницы отображения списка публикаций
	
	Наследуется от Page
*/
public partial class FeedsPage : Page
{
	/// Конструктор класса FeedsPage
	public FeedsPage()
	{
		InitializeComponent();
	}

	/**
		\brief Скрытый метод для регулировки прокрутки на главной странице
		\param[in] sender Элемент (SVScrollMain), у которого сгенерировалось событие
		\param[in] e Аргументы события прокрутки колесика мышки
	*/
	private void SVScrollMain_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScrollMain.ScrollToVerticalOffset(SVScrollMain.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}

	/**
		\brief Скрытый метод для регулировки прокрутки на странице отложенных публикаций
		\param[in] sender Элемент (SVScrollReadLater), у которого сгенерировалось событие
		\param[in] e Аргументы события прокрутки колесика мышки
	*/
	private void SVScrollReadLater_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScrollReadLater.ScrollToVerticalOffset(SVScrollReadLater.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}

	/**
		\brief Скрытый метод для регулировки прокрутки на странице избранных публикаций
		\param[in] sender Элемент (SVScrollFavourites), у которого сгенерировалось событие
		\param[in] e Аргументы события прокрутки колесика мышки
	*/
	private void SVScrollFavourites_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScrollFavourites.ScrollToVerticalOffset(SVScrollFavourites.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}
}
