namespace News.Settings;

public class UserSettings
{
	public int Id { get; set; }
	public string Theme { get; set; } = "Dark";
	public bool Notifications { get; set; } = false;
}
