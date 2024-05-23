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
		DataContext = new SettingsVM();
	}

	private void Window_ActualThemeChanged(object sender, RoutedEventArgs e)
	{
		Debug.WriteLine(ThemeManager.GetActualTheme(this));
	}
}
