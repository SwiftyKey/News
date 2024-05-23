using News.Models.Entities;
using News.ViewModels;
using System.Windows.Media;

namespace News.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для FeedWindow.xaml
	/// </summary>
	public partial class FeedWindow
	{
		public FeedWindowVM FeedWindowVM { get; private set; }

		public FeedWindow(FeedWindowVM feedWindowVM)
		{
			InitializeComponent();
			FeedWindowVM = feedWindowVM;
			DataContext = FeedWindowVM;
		}
	}
}
