using ModernWpf;
using News.ViewModels;
using System.Diagnostics;
using System.Windows;

namespace News.Views.Pages;

/**
	\brief Разделенный класс страницы отображения настроек приложения
	
	Наследуется от Page
*/
public partial class SettingsPage
{
	/// Конструктор класса SettingsPage
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
