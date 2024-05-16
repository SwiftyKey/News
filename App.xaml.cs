using News.Models.Entities;
using News.Models.Repositories;
using News.Settings;
using News.ViewModels.Services;
using System.Windows;

namespace News;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	public static Models.Repositories.AppContext DB { get; set; } = new();
	public static UserService UserService { get; set; } = new UserService(new UserRepository(DB));
	public static FeedService FeedService { get; set; } = new FeedService(new FeedRepository(DB));
	public static SourceService SourceService { get; set; } = new SourceService(new SourceRepository(DB));
	public static AppSettings CurrentAppSettings { get; set; }
	public static User CurrentUser { get; set; }
	public static UserSettings CurrentUserSettings { get; set; }

	Mutex mutex;
	private void App_Startup(object sender, StartupEventArgs e)
	{
		string mutName = "Приложение";
		mutex = new Mutex(true, mutName, out bool createdNew);
		if (!createdNew)
		{
			Shutdown();
		}

		CurrentAppSettings = SettingsSerializer.GetAppSettings().Result;
		CurrentUser = UserService.GetById(CurrentAppSettings.CurrentUserId);
		CurrentUserSettings = SettingsSerializer.GetUserSettingsByLogin(CurrentUser.Login).Result;
	}
}
