using System.IO;
using System.Text.Json;

namespace News.Settings;

public static class SettingsSerializer
{
	public static string SettingsFolder { get; set; } = @"D:\Download\ВУЗ\ООП\News\";
	public static string SettingsPath { get; set; } = SettingsFolder + "app.settings.json";

	private static readonly JsonSerializerOptions options = new()
	{
		WriteIndented = true
	};

	public static void UpdateAppSettings(AppSettings appSettings)
	{
		using (FileStream fs = new(SettingsPath, FileMode.OpenOrCreate))
			JsonSerializer.Serialize(fs, appSettings, options);
	}

	public static AppSettings GetAppSettings()
	{
		using (FileStream fs = new(SettingsPath, FileMode.Open))
			return JsonSerializer.Deserialize<AppSettings>(fs);
	}
}