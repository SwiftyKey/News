using System.Windows;

namespace News.Views.Windows;

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
