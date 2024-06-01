using News.ViewModels;

namespace News.Views.Windows;


/// Разделенный класс окна добавления отображения публикации
public partial class PublicationWindow
{
	/// ViewModel данного окна
	public PublicationWindowVM PublicationWindowVM { get; private set; }

	/// Конструктор класса PublicationWindow
	public PublicationWindow(PublicationWindowVM publicationWindowVM)
	{
		InitializeComponent();
		PublicationWindowVM = publicationWindowVM;
		DataContext = PublicationWindowVM;
	}
}
