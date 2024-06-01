using News.ViewModels;

namespace News.Views.Windows;

public partial class PublicationWindow
{
	public PublicationWindowVM PublicationWindowVM { get; private set; }

	public PublicationWindow(PublicationWindowVM publicationWindowVM)
	{
		InitializeComponent();
		PublicationWindowVM = publicationWindowVM;
		DataContext = PublicationWindowVM;
	}
}
