using News.Views.Windows;
using System.Windows.Controls;

namespace News.Views.Pages;

/// <summary>
/// Логика взаимодействия для SourcesPage.xaml
/// </summary>
public partial class SourcesPage : Page
{
	public SourcesPage()
	{
		InitializeComponent();
	}

	private void BtnAddNewSources_Click(object sender, System.Windows.RoutedEventArgs e)
	{
		var addSourcesWindow = new AddSourcesWindow();
		addSourcesWindow.Show();
    }
}
