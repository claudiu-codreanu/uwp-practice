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

using WeatherTake2.Proxy;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace WeatherTake2
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
			var position = await LocationManager.GetPosition();

			if( position == null )
			{
				WeatherTextBlock.Text = "Could not get access to location services.";
				return;
			}

			WeatherService proxy = new WeatherService();

			//WeatherResponse resp = await proxy.GetWeather();
			WeatherResponse resp = await proxy.GetWeather(position.Coordinate.Latitude, position.Coordinate.Longitude);

			Weather weather = resp.weather[0];

			String info = String.Format("Temp:\t\t{0}\nMain:\t\t{1}\nDescription:\t{2}\nName:\t\t{3}",
				(int)resp.main.temp, weather.main, weather.description, resp.name);

			WeatherTextBlock.Text = info;

			BitmapImage bmp = new BitmapImage();
			bmp.UriSource = new Uri(proxy.GetIconUrl(weather.icon), UriKind.Absolute);

			WeatherImage.Source = bmp;


			var updater = TileUpdateManager.CreateTileUpdaterForApplication();
			var tileXml = TileUpdateManager.GetTemplateContent(TileTemplateType.TileSquareText01);

			var nodes = tileXml.GetElementsByTagName("text");
			nodes[0].InnerText = resp.name;
			nodes[1].InnerText = weather.description;
			nodes[2].InnerText = weather.main;
			nodes[3].InnerText = "Temp: " + resp.main.temp;


			TileNotification notification = new TileNotification(tileXml);
			updater.Update(notification);
		}
	}
}
