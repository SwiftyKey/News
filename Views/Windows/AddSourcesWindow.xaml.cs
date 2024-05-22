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
		}

		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;
		}
	}
}
