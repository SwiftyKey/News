using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace News.Models.Common;

public abstract class BaseChanged : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;
	public void OnPropertyChanged([CallerMemberName] string prop = "")
	{
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
	}
}
