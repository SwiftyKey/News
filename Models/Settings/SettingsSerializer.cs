using System.IO;
using System.Text.Json;

namespace News.Settings;

public static class SettingsSerializer
{
	public static string SettingsPath { get; set; } = @"News\Data\";
	static readonly JsonSerializerOptions options = new()
	{
		WriteIndented = true
	};

	static string FormatPathFile(string login) => SettingsPath + $"{login}.settings.json";

	public static async Task CreateUserSettings(UserSettings us, string login)
	{
		using (FileStream fs = new(FormatPathFile(login), FileMode.OpenOrCreate))
			await JsonSerializer.SerializeAsync(fs, us, options);
	}

	public static async Task UpdateUserSettings(UserSettings us, string login)
	{
		await CreateUserSettings(us, login);
	}

	public static async Task<UserSettings?> GetUserSettingsByLogin(string login)
	{
		using (FileStream fs = new(FormatPathFile(login), FileMode.Open))
			return await JsonSerializer.DeserializeAsync<UserSettings>(fs);
	}
}
