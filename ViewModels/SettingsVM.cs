using ModernWpf;
using News.Models.Common;
using News.Settings;
using News.Utilities;

namespace News.ViewModels;

/**
	\brief Модель представления для работы с настройками приложения

	Наследуется от BaseChanged
*/
public class SettingsVM : BaseChanged
{
	/// Текущие настройки приложения
	public static AppSettings AppSettings { get; set; }

	/// Команда изменения темы приложения (светлая или темная)
	private RelayCommand? themeChangedCommand;
	/// Свойство для работы с themeChangedCommand
	public RelayCommand? ThemeChangedCommand
	{
		get
		{
			return themeChangedCommand ??= new RelayCommand(_ =>
			{
				AppSettings.Theme = AppSettings.Theme == ApplicationTheme.Dark ? ApplicationTheme.Light : ApplicationTheme.Dark;
			});
		}
	}
}
