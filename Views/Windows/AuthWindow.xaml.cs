using News.Models.Entities;
using News.Models.Hashers;
using News.ViewModels;

namespace News.Views.Windows;

public partial class AuthWindow
{
	public AuthWindow()
	{
		InitializeComponent();
	}

	private void ShowMainWindow(string login)
	{
		var mainWindow = new MainWindow(login);
		mainWindow.Show();
		Close();
	}

	private void BtnSignIn_Click(object sender, System.Windows.RoutedEventArgs e)
	{
		var login = TBLogin.Text;

		var user = ApplicationVM.UserService.GetByLogin(login);
		if (user is null)
		{
			user = new User
			{
				Login = login,
				HashPassword = PBPassword.Password
			};

			_ = ApplicationVM.UserService.AddAsync(user);
		}
		else
		{
			if (user.HashPassword != SHA256Hasher.Hash(PBPassword.Password))
			{
				TBPasswordStatus.Text = "Не правильный пароль";
				return;
			}
		}

		ShowMainWindow(login);
	}
}