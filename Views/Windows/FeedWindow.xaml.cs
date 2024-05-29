using News.ViewModels;

namespace News.Views.Windows;


/// Разделенный класс окна добавления отображения публикации
public partial class FeedWindow
{
	/// ViewModel данного окна
	public FeedWindowVM FeedWindowVM { get; private set; }

	/// Конструктор класса FeedWindow
	public FeedWindow(FeedWindowVM feedWindowVM)
	{
		InitializeComponent();
		FeedWindowVM = feedWindowVM;
		DataContext = FeedWindowVM;
	}
}
