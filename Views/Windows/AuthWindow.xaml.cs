using News.Models.Entities;
using News.Models.Hashers;
using News.Settings;
using News.ViewModels.Services;
using System.Windows.Media;

namespace News.Views.Windows;

/// <summary>
/// Логика взаимодействия для AuthWindow.xaml
/// </summary>
public partial class AuthWindow
{
	public AuthWindow()
	{
		InitializeComponent();
		Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
	}

	public void ShowMainWindow()
	{
		MainWindow mainWindow = new();
		mainWindow.Show();
		Close();
	}

	private void BtnSignIn_Click(object sender, System.Windows.RoutedEventArgs e)
	{
		var login = TBLogin.Text;
		var isRemember = CBRememberMe.IsChecked;

		var user = App.UserService.GetByLogin(login);
		if (user is null)
		{
			user = new User
				{
					Login = login,
					HashPassword = PBPassword.Password
				};

			_ = App.UserService.AddAsync(user);
			_ = SettingsSerializer.CreateUserSettings(new UserSettings { Id = user.Id }, login);
		}
        else
        {
             if (user.HashPassword != SHA256Hasher.Hash(PBPassword.Password))
			 {
				TBPasswordStatus.Text = "Не правильный пароль";
				return;
			 }
        }

		var newAppSettings = new AppSettings
		{
			CurrentUserId = user.Id,
			IsRemember = isRemember
		};
		_ = SettingsSerializer.UpdateSettings(newAppSettings);
		App.CurrentAppSettings = newAppSettings;
		App.CurrentUser = App.UserService.GetById(App.CurrentAppSettings.CurrentUserId);
		App.CurrentUserSettings = SettingsSerializer.GetUserSettingsByLogin(App.CurrentUser.Login).Result;

		ShowMainWindow();
    }
}
