using ModernWpf;
using News.Settings;
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

		var newUserSettings = new UserSettings 
			{
				Id=App.CurrentUserSettings.Id,
				Theme= ThemeManager.Current.ApplicationTheme.ToString(),
				Notifications= App.CurrentUserSettings.Notifications
			};
		UpdateUserSettings(newUserSettings);
	}

	private void Window_ActualThemeChanged(object sender, RoutedEventArgs e)
	{
		Debug.WriteLine(ThemeManager.GetActualTheme(this));
	}

	private void TSNotification_Toggled(object sender, RoutedEventArgs e)
	{
		var newUserSettings = new UserSettings
		{
			Id = App.CurrentUserSettings.Id,
			Theme = App.CurrentUserSettings.Theme,
			Notifications = TSNotification.IsOn
		};
		UpdateUserSettings(newUserSettings);
	}

	private void UpdateUserSettings(UserSettings userSettings)
	{
		App.CurrentUserSettings = userSettings;
		_ = SettingsSerializer.UpdateUserSettings(userSettings, App.CurrentUser.Login);
	}

	private void BtnLogOut_Click(object sender, RoutedEventArgs e)
	{
		App.CurrentAppSettings.IsRemember = false;
		_ = SettingsSerializer.UpdateSettings(App.CurrentAppSettings);
		Application.Current.Shutdown();
	}
}
