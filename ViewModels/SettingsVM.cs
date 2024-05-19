using ModernWpf;
using News.Models.Common;
using News.Settings;
using News.Utilities;

namespace News.ViewModels;

public class SettingsVM : BaseChanged
{
	public static AppSettings AppSettings { get; set; }

	private RelayCommand? themeChangedCommand;
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
