using News.Models.Entities;
using News.Models.Hashers;
using News.ViewModels;
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

		ApplicationVM.CurrentUser = user;

		ShowMainWindow();
	}
}
