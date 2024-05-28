using Microsoft.Toolkit.Uwp.Notifications;
using News.ViewModels;
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
