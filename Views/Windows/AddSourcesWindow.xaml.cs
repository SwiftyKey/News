using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace News.Views.Windows
{
	/// <summary>
	/// Логика взаимодействия для AddSourcesWindow.xaml
	/// </summary>
	public partial class AddSourcesWindow
	{
		public AddSourcesWindow()
		{
			InitializeComponent();
			Icon = new ImageSourceConverter().ConvertFrom(Properties.Resources.WindowSidebar) as ImageSource;
		}

		private void BtnSearch_Click(object sender, RoutedEventArgs e)
		{
			string sourceUrl = "";
			var sources = FeedReader.GetFeedUrlsFromUrlAsync(TBSourceLink.Text).Result;
			if (sources.Count() < 1) sourceUrl = TBSourceLink.Text;
			else sourceUrl = sources.First().Url;
			var readerTask = FeedReader.ReadAsync(sourceUrl).Result;
			SBResult.Text = $"Источник \'{readerTask.Title}\' добавлен";
		}
	}
}
