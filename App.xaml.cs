﻿using Microsoft.Toolkit.Uwp.Notifications;
using News.ViewModels;
using System.Windows;

namespace News;

/**
	\brief Разделенный класс приложения
	
	Наследуется от Application
*/
public partial class App : Application
{
	/**
		\brief Скрытый метод, выполняющийся при запуске приложения
		\param[in] sender Элемент (App), у которого сгенерировалось событие
		\param[in] e Аргументы события запуска

		Следит за тем, чтобы программа выполнялась в одном потоке, а так же обрабатывает нажатие на всплывающее уведомление
	*/
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
