using News.Models.Common;
using News.Models.Entities;
using News.Utilities;
using System.Collections.ObjectModel;

namespace News.ViewModels;

class SourceVM : BaseChangedEntity
{
	Source selectedSource;
	public Source SelectedSource
	{
		get { return selectedSource; }
		set
		{
			selectedSource = value;
			OnPropertyChanged(nameof(SelectedSource));
		}
	}

	public ObservableCollection<Source> Sources { get; set; } = [];

	private RelayCommand addCommand;
	public RelayCommand AddCommand
	{
		get
		{
			return addCommand ??
				(addCommand = new RelayCommand(obj =>
				{
					Source source = new Source();
					Sources.Insert(0, source);
					SelectedSource = source;
				}));
		}
	}


	private RelayCommand removeCommand;
	public RelayCommand RemoveCommand
	{
		get
		{
			return removeCommand ??
				(removeCommand = new RelayCommand(obj =>
				{
					Source source = obj as Source;
					if (source != null)
						Sources.Remove(source);
				},
				(obj) => Sources.Count > 0));
		}
	}
}
