using ModernWpf.Controls;
using News.Settings;
using News.ViewModels;
using News.Views.Pages;
using System.Windows;
using System.Windows.Threading;

/**
	\brief Пространство имен, в котором содержатся основные классы приложения
	\param Содержит классы:
		@ref MainWindow
		@ref App
*/
namespace News;

/// Разделенный класс главного окна
public partial class MainWindow
{
	/// История перемещения по страницам главного окна
	private LinkedList<NavigationViewItem> History { get; set; }
	/// Булево значение, обозначающее, была ли нажата кнопка возвращения назад
	private bool IsGoBack { get; set; } = false;

	/// Конструктор класса MainWindow
	public MainWindow()
	{
		InitializeComponent();

		DataContext = new ApplicationVM();

		History = [];
		NavView.SelectedItem = NVItemAllNews;

		var timer = new DispatcherTimer
		{
			Interval = ApplicationVM.Observer.UpdateFreq.ToTimeSpan()
		};
		timer.Tick += timer_Tick;
		timer.Start();
	}

	/**
		\brief Скрытый метод, который вызывает метод Update у класса Observer по истечению времени
		\param[in] sender Элемент (timer), у которого сгенерировалось событие
		\param[in] e Аргументы события
	*/
	private void timer_Tick(object sender, EventArgs e)
	{
		_ = ApplicationVM.Observer.Update();
	}

	/**
		\brief Скрытый метод, который обрабатывает изменение выбора NavView
		\param[in] sender Элемент (NavView), у которого сгенерировалось событие
		\param[in] args Аргументы данного события
	*/
	private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
	{
		var selectedItem = (NavigationViewItem)args.SelectedItem;

		if (!IsGoBack)
			History.AddLast(selectedItem);
		else
			History.RemoveLast();

		if (args.IsSettingsSelected)
			mainFrame.Navigate(typeof(SettingsPage));
		else
		{
			if (selectedItem != null)
			{
				var selectedItemTag = (string)selectedItem.Tag;
				var page = Type.GetType("News.Views.Pages." + selectedItemTag);
				mainFrame.Navigate(page);
			}
		}
	}

	/**
		\brief Скрытый метод, который обрабатывает возвращение на предыдущую страницу
		\param[in] sender Элемент (NavView), у которого сгенерировалось событие
		\param[in] args Аргументы данного события
	*/
	private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
	{
		if (mainFrame.CanGoBack && History?.Last?.Previous is not null)
		{
			IsGoBack = true;
			NavView.SelectedItem = History.Last.Previous.Value;
			IsGoBack = false;
		}
    }

	/**
		\brief Скрытый метод, который загружает сохраненные настройки приложения при загрузке главного окна
		\param[in] sender Элемент (Window), у которого сгенерировалось событие
		\param[in] e Аргументы перенаправленного события
	*/
	private void Window_Loaded(object sender, RoutedEventArgs e)
	{
		SettingsVM.AppSettings = SettingsSerializer.GetAppSettings();
	}

	/**
		\brief Скрытый метод, который сохраняет текущие настройки приложения при закрытие окна
		\param[in] sender Элемент (Window), у которого сгенерировалось событие
		\param[in] e Аргументы данного события
	*/
	private void Window_Closed(object sender, EventArgs e)
	{
		SettingsSerializer.UpdateAppSettings(SettingsVM.AppSettings);
	}

	/**
		\brief Скрытый метод, который прячет главное окно при нажатии кнопки свернуть
		\param[in] sender Элемент (Window), у которого сгенерировалось событие
		\param[in] e Аргументы данного события
	*/
	private void Window_StateChanged(object sender, EventArgs e)
	{
		if (WindowState == WindowState.Minimized)
			Hide();
	}

	/**
		\brief Скрытый метод, который показывает главное окно, при нажатии на иконку приложения в панели задач
		\param[in] sender Элемент (TaskBar), у которого сгенерировалось событие
		\param[in] e Аргументы данного события
	*/
	private void TaskBar_TrayLeftMouseDown(object sender, EventArgs e)
	{
		Show();
	}
}