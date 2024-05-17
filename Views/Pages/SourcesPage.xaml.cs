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

	private void SVScroll_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScroll.ScrollToVerticalOffset(SVScroll.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}
}
