using ModernWpf.Controls;
using News.Views.Pages;
using System.Windows.Media;

namespace News;

public partial class MainWindow
{
	LinkedList<NavigationViewItem> History { get; set; }
	bool IsGoBack { get; set; } = false;

	public MainWindow()
	{
		InitializeComponent();

		History = [];
		NavView.SelectedItem = NVItemAllNews;
		Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
	}

	private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
	{
		var selectedItem = (NavigationViewItem)args.SelectedItem;

		if (!IsGoBack)
			History.AddLast(selectedItem);
		else
			History.RemoveLast();

		if (args.IsSettingsSelected)
			mainFrame.Navigate(typeof(SettingsPage));
		else
		{
			if (selectedItem != null)
			{
				var selectedItemTag = (string)selectedItem.Tag;
				var page = Type.GetType("News.Views.Pages." + selectedItemTag);
				mainFrame.Navigate(page);
			}
		}
	}

	private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
	{
		if (mainFrame.CanGoBack && History?.Last?.Previous is not null)
		{
			IsGoBack = true;
			NavView.SelectedItem = History.Last.Previous.Value;
			IsGoBack = false;
		}
    }
}