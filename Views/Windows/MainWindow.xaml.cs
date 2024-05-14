using News.Views.Pages;
using System.Windows.Media;

namespace News;

public partial class MainWindow
{
	public MainWindow()
	{
		InitializeComponent();
		contentFrame.Navigate(typeof(FeedsPage));
		Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
	}

	private void NavView_SelectionChanged(ModernWpf.Controls.NavigationView sender, ModernWpf.Controls.NavigationViewSelectionChangedEventArgs args)
	{
		if (args.IsSettingsSelected)
		{
			contentFrame.Navigate(typeof(SettingsPage));
		}
		else
		{
			contentFrame.Navigate(typeof(FeedsPage));
		}
	}
}