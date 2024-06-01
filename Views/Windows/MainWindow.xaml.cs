using ModernWpf.Controls;
using News.ViewModels;
using News.Views.Pages;
using System.Windows;
using System.Windows.Threading;

namespace News;

public partial class MainWindow
{
	private LinkedList<NavigationViewItem> History { get; set; }
	private bool IsGoBack { get; set; } = false;

	public MainWindow(string login)
	{
		InitializeComponent();

		DataContext = new ApplicationVM(login);

		History = [];
		NavView.SelectedItem = NVItemAllNews;

		var timer = new DispatcherTimer
		{
			Interval = ApplicationVM.Observer.UpdateFreq.ToTimeSpan()
		};
		timer.Tick += timer_Tick;
		timer.Start();
	}

	private void timer_Tick(object sender, EventArgs e)
	{
		_ = ApplicationVM.Observer.Update();
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

	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		ModernWpf.ThemeManager.Current.ApplicationTheme = ApplicationVM.CurrentUser.Theme;
	}

	private void Window_Closed(object sender, EventArgs e) => ApplicationVM.DB.SaveChanges();

	private void Window_StateChanged(object sender, EventArgs e)
	{
		if (WindowState == WindowState.Minimized)
			Hide();
	}

	private void TaskBar_TrayLeftMouseDown(object sender, EventArgs e)
	{
		Show();
	}
}