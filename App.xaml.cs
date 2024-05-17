using News.Models.Entities;
using News.Models.Repositories;
using News.ViewModels.Services;
using System.Windows;

namespace News;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
	Mutex mutex;
	private void App_Startup(object sender, StartupEventArgs e)
	{
		string mutName = "Приложение";
		mutex = new Mutex(true, mutName, out bool createdNew);
		if (!createdNew)
		{
			Shutdown();
		}
	}
}
