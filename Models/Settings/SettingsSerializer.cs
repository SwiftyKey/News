using System.IO;
using System.Text.Json;

namespace News.Settings;

/**
	\brief Статический класс, предназначенный для работы с  настройками приложения
*/
public static class SettingsSerializer
{
	/// Путь к папке настроек
	public static string SettingsFolder { get; set; } = @"D:\Download\ВУЗ\ООП\News\";

	/// Путь к файлу настроек
	public static string SettingsPath { get; set; } = SettingsFolder + "app.settings.json";

	/// Опции сериализации
	private static readonly JsonSerializerOptions options = new()
	{
		WriteIndented = true
	};

	/**
		\brief Статический метод, обновляющий настройки приложения
		\param[in] appSettings Объект настроек
	*/
	public static void UpdateAppSettings(AppSettings appSettings)
	{
		using (FileStream fs = new(SettingsPath, FileMode.OpenOrCreate))
			JsonSerializer.Serialize(fs, appSettings, options);
	}

	/**
		\brief Статический метод, получающий настройки приложения
		\return Объект настроек
	*/
	public static AppSettings GetAppSettings()
	{
		using (FileStream fs = new(SettingsPath, FileMode.Open))
			return JsonSerializer.Deserialize<AppSettings>(fs);
	}
}