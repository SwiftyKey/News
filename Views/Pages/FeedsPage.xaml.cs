using System.Windows.Controls;

namespace News.Views.Pages;

/// <summary>
/// Логика взаимодействия для FeedsPage.xaml
/// </summary>
public partial class FeedsPage : Page
{
	public FeedsPage()
	{
		InitializeComponent();
	}

	private void SVScrollMain_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScrollMain.ScrollToVerticalOffset(SVScrollMain.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}

	private void SVScrollReadLater_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScrollReadLater.ScrollToVerticalOffset(SVScrollReadLater.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}

	private void SVScrollFavourites_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
	{
		SVScrollFavourites.ScrollToVerticalOffset(SVScrollFavourites.VerticalOffset - (e.Delta > 0 ? 20 : -20));
		e.Handled = true;
	}
}
