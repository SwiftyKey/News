using System.Windows;

/**
	\brief Пространство имен, в котором содержатся окна приложения
	\param Содержит классы:
		@ref AddSourcesWindow
		@ref FeedWindow
*/
namespace News.Views.Windows;


/// Разделенный класс окна добавления источника (является диалоговым окном)
public partial class AddSourcesWindow
{
	/// Конструктор класса AddSourcesWindow
	public AddSourcesWindow()
	{
		InitializeComponent();
	}

	/**
		\brief Скрытый метод, который устанавливает, что диалог был отработан
		\param[in] sender Элемент (BtnSearch), у которого сгенерировалось событие
		\param[in] e Аргументы перенаправленного события
	*/
	private void BtnSearch_Click(object sender, RoutedEventArgs e)
	{
		DialogResult = true;
	}
}
