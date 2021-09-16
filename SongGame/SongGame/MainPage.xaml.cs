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
using SongGame.Model;
using Windows.Storage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SongGame
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Song> songs;
        public MainPage()
        {
            this.InitializeComponent();
            songs = new ObservableCollection<Song>();
        }

        public ObservableCollection<Song> Songs
        {
            get { return songs; }
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            MyProgressRing.IsActive = true;

            var randoms = await Song.PickRandomSongs(await Song.AllSongs());
            randoms.ForEach( s => songs.Add(s));

            MyProgressRing.IsActive = false;
        }

        private async void SongsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Song song = e.ClickedItem as Song;
            MyMediaElement.SetSource(await song.File.OpenAsync(FileAccessMode.Read),
                                song.File.ContentType);
        }

        private void PlayAgainButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
