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

	private void BtnSignIn_Click(object sender, System.Windows.RoutedEventArgs e)
	{
		MainWindow mainWindow = new();
		mainWindow.Show();
		Close();
        }
    }
