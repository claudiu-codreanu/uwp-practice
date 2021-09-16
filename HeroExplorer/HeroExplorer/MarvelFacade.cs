using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Security.Cryptography;
using Windows.Security.Cryptography.Core;
using Windows.Storage.Streams;
using Newtonsoft.Json;
using HeroExplorer.Models;
using System.Collections.ObjectModel;

namespace HeroExplorer
{
    public class MarvelFacade
    {
        private const string PublicKey = "3ae74618e36203a440f81cf34a28d126";
        private const string PrivateKey = "4623e1a9a21f5ceec3244bb9b9beab6313f627a3";
        private const int MaxCharacters = 1500;

        public async static Task GetCharacterList( ObservableCollection<Character> characters )
        {
            var offset = new Random().Next(MaxCharacters);
            var timestamp = DateTime.Now.Ticks;
            var hash = ComputeMD5Hash(timestamp.ToString() + PrivateKey + PublicKey);


            HttpClient http = new HttpClient();
            string url = String.Format("http://gateway.marvel.com:80/v1/public/characters?limit=10&offset={0}&ts={1}&apikey={2}&hash={3}",
                                               offset, timestamp, PublicKey, hash);

            var json = await http.GetStringAsync(url);
            CharacterDataWrapper wrapper = JsonConvert.DeserializeObject(json, typeof(CharacterDataWrapper)) as CharacterDataWrapper;

            //characters.Clear();
            wrapper.data.results
                .FindAll(p => !p.thumbnail.path.Contains("image_not_available"))
                .ForEach(p => characters.Add(p));
        }


        public async static Task GetComicList(ObservableCollection<ComicBook> comics, int characterId)
        {
            var timestamp = DateTime.Now.Ticks;
            var hash = ComputeMD5Hash(timestamp.ToString() + PrivateKey + PublicKey);

            HttpClient http = new HttpClient();

            int limit = 10;

            string url = String.Format("https://gateway.marvel.com:443/v1/public/comics?characters={0}&limit={1}&apikey={2}&ts={3}&hash={4}",
                                        characterId, limit, PublicKey, timestamp, hash);

            var json = await http.GetStringAsync(url);
            ComicDataWrapper wrapper = JsonConvert.DeserializeObject(json, typeof(ComicDataWrapper)) as ComicDataWrapper;

            comics.Clear();
            wrapper.data.results
                .FindAll(p => !p.thumbnail.path.Contains("image_not_available"))
                .ForEach(p => comics.Add(p));
        }


        private static string ComputeMD5Hash(string data)
        {
            var algorithm = HashAlgorithmProvider.OpenAlgorithm(HashAlgorithmNames.Md5);
            var buffer = CryptographicBuffer.ConvertStringToBinary(data, BinaryStringEncoding.Utf8);

            var digest = algorithm.HashData(buffer);
            return CryptographicBuffer.EncodeToHexString(digest);
        }
    }
}
