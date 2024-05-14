namespace News.Settings;

public class UserSettings
{
	public int Id { get; set; }
	public string Theme { get; set; } = null!;
	public bool Notifications { get; set; } = false;
	public bool Remember { get; set; } = false;
}
