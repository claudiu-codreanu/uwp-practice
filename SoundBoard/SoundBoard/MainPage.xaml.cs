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

using SoundBoard.Model;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SoundBoard
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
		private List<SoundCategory> soundCategories;
		private ObservableCollection<SoundItem> sounds;
		private Stack<string> history;
		private string lastCategSelected;

        public MainPage()
        {
            this.InitializeComponent();

			soundCategories = SoundCategory.AllCategories;

			sounds = new ObservableCollection<SoundItem>();
			SoundItem.GetSounds(sounds);

			history = new Stack<string>();
        }

		private void HamburgerButton_Click(object sender, RoutedEventArgs e)
		{
			MainSplitView.IsPaneOpen = !MainSplitView.IsPaneOpen;
		}

		private async void BackButton_Click(object sender, RoutedEventArgs e)
		{
			System.Net.Http.HttpClient http = new System.Net.Http.HttpClient();
			string html = await http.GetStringAsync("http://www.google.com");
			//HtmlTextBlock.Text = html;

			//return;

			if(history.Count == 0)
			{
				lastCategSelected = null;
				SoundItem.GetSounds(sounds);
				CurrentSoundsTextBlock.Text = "All Sounds";
				OptionsListView.SelectedItem = null;
			}
			else
			{
				string category = history.Pop();

				SoundItem.GetSounds(sounds, category);
				CurrentSoundsTextBlock.Text = category;

				OptionsListView.SelectedItem = OptionsListView.Items.First(p => (p as SoundCategory).Name == category);
			}
		}

		public List<SoundCategory> Categories
		{
			get { return soundCategories; }
		}


		public ObservableCollection<SoundItem> Sounds
		{
			get { return sounds; }
		}

		private void CurrentSoundsGridView_ItemClick(object sender, ItemClickEventArgs e)
		{
			PlaySound(e.ClickedItem as SoundItem);
		}

		private void OptionsListView_ItemClick(object sender, ItemClickEventArgs e)
		{
			if( lastCategSelected != null )
			{
				history.Push(lastCategSelected);
			}

			MainSplitView.IsPaneOpen = false;

			SoundCategory categ = e.ClickedItem as SoundCategory;
			SoundItem.GetSounds(sounds, categ.Name);

			CurrentSoundsTextBlock.Text = categ.Name;
			lastCategSelected = categ.Name;
		}

		private void SearchAutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
		{
			if( args.Reason == AutoSuggestionBoxTextChangeReason.UserInput )
			{
				var matches = from snd in SoundItem.AllSounds
							  where snd.Name.ToLower().StartsWith(SearchAutoSuggestBox.Text.ToLower())
							  select snd.Name;

				SearchAutoSuggestBox.ItemsSource = matches;
			}
		}

		private void SearchAutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
		{

		}

		private void SearchAutoSuggestBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
		{
			SoundItem.GetSoundsByName(sounds, args.QueryText);

			//var name = args.QueryText.ToLower();
			//SoundItem snd = SoundItem.AllSounds.Find(p => p.Name.ToLower() == name);

			//if (snd != null)
			//{
			//	PlaySound(snd);
			//}
		}


		private void PlaySound( SoundItem snd )
		{
			//PlayMediaElement.Source = new System.Uri("ms-appx://" + snd.Sound);
			PlayMediaElement.Source = new Uri(this.BaseUri, snd.Sound);
		}
	}
}
