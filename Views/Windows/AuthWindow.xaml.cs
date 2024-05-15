using News.Models.Repositories;
using News.Models.Entities;
using News.ViewModels.Services;
using System.Windows.Media;
using News.Models.Hashers;
using News.Settings;

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

		_ = SettingsSerializer.UpdateSettings(new AppSettings
			{
				CurrentUserId = user.Id,
				IsRemember=isRemember
			});

		ShowMainWindow();
    }

	public void ShowMainWindow()
	{
		MainWindow mainWindow = new();
		mainWindow.Show();
		Close();
	}
}
