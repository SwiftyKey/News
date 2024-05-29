using Microsoft.Toolkit.Uwp.Notifications;
using News.ViewModels;
using System.Windows;

namespace News;

/**
	\brief Разделенный класс приложения
	
	Наследуется от Application
*/
public partial class App : Application
{
	/// Поток программы
	private Mutex mutex;

	/**
		\brief Скрытый метод, выполняющийся при запуске приложения
		\param[in] sender Элемент (App), у которого сгенерировалось событие
		\param[in] e Аргументы события запуска

		Следит за тем, чтобы программа выполнялась в одном потоке, а так же обрабатывает нажатие на всплывающее уведомление
	*/
	private void App_Startup(object sender, StartupEventArgs e)
	{
		string mutName = "News";
		mutex = new Mutex(true, mutName, out bool createdNew);
		if (!createdNew)
		{
			Shutdown();
		}

		ToastNotificationManagerCompat.OnActivated += toastArgs =>
		{
			var args = ToastArguments.Parse(toastArgs.Argument);

			if (args.Contains("FeedId"))
			{
				var feedId = int.Parse(args["FeedId"]);
				var feed = ApplicationVM.FeedService.GetById(feedId);

				if (args.Contains("Action") && args["Action"] == "ViewFeed")
					if (ApplicationVM.ViewFeedCommand?.CanExecute(feed) == true)
						ApplicationVM.ViewFeedCommand.Execute(feed);
			}
		};
	}
}
