using ModernWpf;
using News.Models.Common;
using System.Text.Json.Serialization;

namespace News.Settings;

public class AppSettings : BaseChanged
{
	[JsonIgnore]
	private bool notificationsOn = false;
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
	private ApplicationTheme theme;
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