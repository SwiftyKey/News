using ModernWpf;
using News.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace News.Views.Pages;

/// <summary>
/// Логика взаимодействия для SettingsPages.xaml
/// </summary>
public partial class SettingsPage
{
    public SettingsPage()
    {
		InitializeComponent();
	}

	private void ToggleTheme(object sender, RoutedEventArgs e)
	{
		if (ThemeManager.Current.ActualApplicationTheme == ApplicationTheme.Dark)
			ThemeManager.Current.ApplicationTheme = ApplicationTheme.Light;
		else
			ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
	}

	private void Window_ActualThemeChanged(object sender, RoutedEventArgs e)
	{
		Debug.WriteLine(ThemeManager.GetActualTheme(this));
	}

	private void TSNotification_Toggled(object sender, RoutedEventArgs e)
	{
		ApplicationVM.NotificationsOn = TSNotification.IsOn;
	}
}
