using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using Newtonsoft.Json;

namespace WeatherTake2.Proxy
{
	public class WeatherService
	{
		public async Task<WeatherResponse> GetWeather(double lat = 26.01, double lon = -80.18)
		{
			string key = "6a5b4e13fed3483d8d1d4e6774336eb4";
			string url = String.Format("http://api.openweathermap.org/data/2.5/weather?lat={0}&lon={1}&units=imperial&appid={2}",
				lat, lon, key);

			HttpClient http = new HttpClient();
			string json = await http.GetStringAsync(new Uri(url, UriKind.Absolute));

			return JsonConvert.DeserializeObject<WeatherResponse>(json);
		}

		public string GetIconUrl(string iconId)
		{
			return String.Format("http://openweathermap.org/img/w/{0}.png", iconId);
		}
	}
}
