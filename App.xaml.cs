using Microsoft.Toolkit.Uwp.Notifications;
using News.ViewModels;
using System.Windows;

namespace News;

public partial class App : Application
{
	private void App_Startup(object sender, StartupEventArgs e)
	{
		var mutex = new Mutex(true, "News", out bool createdNew);
		if (!createdNew) Shutdown();

		ToastNotificationManagerCompat.OnActivated += toastArgs =>
		{
			var args = ToastArguments.Parse(toastArgs.Argument);

			if (args.Contains("PublicationId"))
			{
				var publicationId = int.Parse(args["PublicationId"]);
				var publication = ApplicationVM.PublicationService.GetById(publicationId);

				if (args.Contains("Action") && args["Action"] == "ViewPublication")
					if (ApplicationVM.ViewPublicationCommand?.CanExecute(publication) == true)
						ApplicationVM.ViewPublicationCommand.Execute(publication);
			}
		};
	}
}
