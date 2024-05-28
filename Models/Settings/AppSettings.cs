using ModernWpf;
using News.Models.Common;
using System.Text.Json.Serialization;

/**
	\brief Пространство имен, в котором содержатся классы для работы с настройками приложения
	\param Содержит классы:
		@ref AppSettings
		@ref SettingsSerializer
*/
namespace News.Settings;

/**
	\brief Класс, предназначенный для создания настроек приложения
	
	Наследуется от BaseChanged
*/
public class AppSettings : BaseChanged
{
	[JsonIgnore]
	/// Булево значение, показывающее, включены ли в приложении настройки
	private bool notificationsOn = false;
	/// Свойство для работы с notificationsOn
	public bool NotificationsOn
	{
		get => notificationsOn;
		set
		{
			notificationsOn = value;
			OnPropertyChanged(nameof(NotificationsOn));
		}
	}

	[JsonIgnore]
	/// Текущая тема приложения
	private ApplicationTheme theme;
	/// Свойство для работы с theme
	public ApplicationTheme Theme
	{
		get => theme;
		set
		{
			theme = value;
			OnPropertyChanged(nameof(Theme));
			ThemeManager.Current.ApplicationTheme = theme;
		}
	}
}