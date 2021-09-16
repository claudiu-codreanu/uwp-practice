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

using Weather.Proxy;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Weather
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

		private async void GetWeatherButton_Click(object sender, RoutedEventArgs e)
		{
			//var position = await LocationManager.GetPosition();

			//if( position == null )
			//{
			//	WeatherTextBlock.Text = "Could not get access to location services.";
			//	return;
			//}

			WeatherService service = new WeatherService();
			//WeatherResponse resp = await service.GetWeather( position.Coordinate.Latitude, position.Coordinate.Longitude );
			WeatherResponse resp = await service.GetWeather();

			Weather.Proxy.Weather weather = resp.weather[0];

			String info = String.Format("Temp:\t\t{0}\nMain:\t\t{1}\nDescription:\t{2}",
				(int)resp.main.temp, weather.main, weather.description);

			WeatherTextBlock.Text = info;

			BitmapImage bmp = new BitmapImage();
			bmp.UriSource = new Uri(service.GetIconUrl(weather.icon), UriKind.Absolute);

			WeatherImage.Source = bmp;
		}
	}
}
