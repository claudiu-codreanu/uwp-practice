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
using System.Collections.ObjectModel;
using HeroExplorer.Models;
using Windows.UI.Xaml.Media.Imaging;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace HeroExplorer
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ObservableCollection<Character> RandomCharacters;
        public ObservableCollection<ComicBook> ComicBooks;

        public MainPage()
        {
            this.InitializeComponent();
            RandomCharacters = new ObservableCollection<Character>();
            ComicBooks = new ObservableCollection<ComicBook>();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var storageFile = await Windows.Storage.StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appx:///VoiceCommandDictionary.xml"));
            await Windows.ApplicationModel.VoiceCommands.VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(storageFile);

            await Refresh();
        }

        public async Task Refresh()
        {
            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            ClearCharacterDetails();
            RandomCharacters.Clear();

            ComicBooks.Clear();
            ClearBookDetails();

            while (RandomCharacters.Count < 10)
            {
                Task t = MarvelFacade.GetCharacterList(RandomCharacters);
                await t;

                MyProgressRing.IsActive = false;
                MyProgressRing.Visibility = Visibility.Collapsed;
            }
        }

        private async void MainListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Character hero = (Character)e.ClickedItem;

            BitmapImage img = new BitmapImage();
            img.UriSource = new Uri(hero.thumbnail.large, UriKind.Absolute);

            DetailsImage.Source = img;

            DetailsNameTextBox.Text = hero.name;
            DetailsDescriptionTextBox.Text = hero.description;



            ComicBooks.Clear();
            ClearBookDetails();


            MyProgressRing.IsActive = true;
            MyProgressRing.Visibility = Visibility.Visible;

            await MarvelFacade.GetComicList(ComicBooks, hero.id);

            MyProgressRing.IsActive = false;
            MyProgressRing.Visibility = Visibility.Collapsed;
        }

        private void ClearCharacterDetails()
        {
            DetailsImage.Source = null;
            DetailsNameTextBox.Text = "";
            DetailsDescriptionTextBox.Text = "";
        }

        private void ClearBookDetails()
        {
            BookTitleTextBlock.Text = "";
            BookDescriptionTextBlock.Text = "";
            BookImage.Source = null;
        }

        private void BooksGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ComicBook book = (ComicBook)e.ClickedItem;

            BookTitleTextBlock.Text = book.title;
            BookDescriptionTextBlock.Text = book.description != null ? book.description : "";

            BitmapImage img = new BitmapImage();
            img.UriSource = new Uri(book.thumbnail.large, UriKind.Absolute);

            BookImage.Source = img;
        }
    }
}
