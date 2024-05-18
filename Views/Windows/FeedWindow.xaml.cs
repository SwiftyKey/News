using News.Models.Entities;
using System.Windows.Media;

namespace News.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для FeedWindow.xaml
	/// </summary>
	public partial class FeedWindow
	{
		public Feed Feed { get; private set; }

		public FeedWindow(Feed feed)
		{
			InitializeComponent();
			Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
			Feed = feed;
			DataContext = Feed;
		}
	}
}
