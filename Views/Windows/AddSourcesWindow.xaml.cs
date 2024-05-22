using System.Windows;
using System.Windows.Media;

namespace News.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для AddSourcesWindow.xaml
	/// </summary>
	public partial class AddSourcesWindow
	{
		public AddSourcesWindow()
		{
			InitializeComponent();
			Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
		}

		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
