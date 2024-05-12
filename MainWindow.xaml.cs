using News.Views.Pages;
using System.Windows.Media.Imaging;

namespace News
{
	public partial class MainWindow
	{
		public MainWindow()
		{
			InitializeComponent();
			contentFrame.Navigate(typeof(FeedsPage));
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
}