using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using News.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace News
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		private List<NewsItem> newsItems;

        public MainPage()
        {
            this.InitializeComponent();

			newsItems = NewsItemsFeed.GetNews();
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            HamburgerSplitView.IsPaneOpen = !HamburgerSplitView.IsPaneOpen;
        }

        private void HamburgerListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			//string category = (sender as ListBox).SelectedItem == FinancialListItem ? "Financial" : "Food";
			string category = FinancialListItem.IsSelected ? "Financial" : "Food";
			TitleTextBlock.Text = category;

			NewsGridView.ItemsSource = FilterNews(category);

			HamburgerSplitView.IsPaneOpen = false;
        }

		private List<NewsItem> News
		{
			get { return FilterNews("Financial"); }
		}


		private List<NewsItem> FilterNews(string category)
		{
			var items = from item in newsItems
						where item.Category == category
						select item;

			return new List<NewsItem>(items);


			//return newsItems.Where(p => p.Category == category).ToList<NewsItem>();
		}
    }
}
