using System.IO;
using System.Text.Json;

namespace News.Settings;

public static class SettingsSerializer
{
	public static string SettingsFolder { get; set; } = @"D:\Download\ВУЗ\ООП\News\Data\";
	public static string SettingsPath { get; set; } = SettingsFolder + "app.settings.json";

	static readonly JsonSerializerOptions options = new()
	{
		WriteIndented = true
	};

	static string FormatUserPathFile(string login) => SettingsFolder + $"{login}.settings.json";

	public static async Task CreateUserSettings(UserSettings us, string login)
	{
		using (FileStream fs = new(FormatUserPathFile(login), FileMode.OpenOrCreate))
			await JsonSerializer.SerializeAsync(fs, us, options);
	}

	public static async Task UpdateUserSettings(UserSettings us, string login)
	{
		await CreateUserSettings(us, login);
	}

	public static async Task<UserSettings?> GetUserSettingsByLogin(string login)
	{
		using (FileStream fs = new(FormatUserPathFile(login), FileMode.Open))
			return await JsonSerializer.DeserializeAsync<UserSettings>(fs);
	}

	public static async Task UpdateSettings(AppSettings appSettings)
	{
		using (FileStream fs = new(SettingsPath, FileMode.OpenOrCreate))
			await JsonSerializer.SerializeAsync(fs, appSettings, options);
	}

	public static async Task<AppSettings?> GetAppSettings()
	{
		using (FileStream fs = new(SettingsPath, FileMode.Open))
			return await JsonSerializer.DeserializeAsync<AppSettings>(fs);
	}
}
