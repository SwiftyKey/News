using CodeHollow.FeedReader;
using System.Windows;
using System.Windows.Media;
using News.Models.Entities;

namespace News.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для AddSourcesWindow.xaml
	/// </summary>
	public partial class AddSourcesWindow
	{
		public Source Source { get; private set; }

		public AddSourcesWindow(Source source)
		{
			InitializeComponent();
			Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
			Source = source;
			DataContext = Source;
		}

		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
