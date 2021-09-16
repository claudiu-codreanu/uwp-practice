using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Storage.FileProperties;

namespace SongGame.Model
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Album { get; set; }
        public BitmapImage Cover { get; set; }
		public StorageFile File { get; set; }


		public async static Task<List<StorageFile>> AllSongs()
		{
			List<StorageFile> allSongs = new List<StorageFile>();

			StorageFolder root = KnownFolders.MusicLibrary;
			await RetrieveSongs(allSongs, await root.GetFolderAsync("UWP"));

			return allSongs;
		}

		private async static Task RetrieveSongs( List<StorageFile> songs, StorageFolder parent )
		{
			foreach( var file in await parent.GetFilesAsync() )
			{
				if (file.FileType == ".mp3")
				{
					songs.Add(file);
				}
			}


			foreach( var folder in await parent.GetFoldersAsync())
			{
				await RetrieveSongs(songs, folder);
			}
		}


		public async static Task<List<Song>> PickRandomSongs(List<StorageFile> allSongs)
		{
			List<Song> randomSongs = new List<Song>();
			List<string> randomAlbums = new List<string>();

			Random r = new Random();
			int id = 0;

			while (randomSongs.Count < 10)
			{
				int i = r.Next(allSongs.Count - 1);
				StorageFile song = allSongs[i];

				var properties = await song.Properties.GetMusicPropertiesAsync();

				if( properties.Album == null)
				{
					continue;
				}

				if( randomAlbums.Contains(properties.Album) )
				{
					continue;
				}

				randomSongs.Add(await SongFromFile(id++, song));
				randomAlbums.Add(properties.Album);
			}

			return randomSongs;
		}


		private async static Task<Song> SongFromFile( int id, StorageFile file )
		{
			Song song = new Song();
			var music = await file.Properties.GetMusicPropertiesAsync();

			song.Id = id;
			song.Title = music.Title;
			song.Artist = music.Artist;
			song.Album = music.Album;
			song.File = file;

			song.Cover = new BitmapImage();
			song.Cover.SetSource(await file.GetThumbnailAsync(ThumbnailMode.MusicView, 200, ThumbnailOptions.UseCurrentScale));

			return song;
		}
    }
}
