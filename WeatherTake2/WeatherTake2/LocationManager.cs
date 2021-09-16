using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Windows.Devices.Geolocation;

namespace WeatherTake2
{
	public class LocationManager
	{
		public async static Task<Geoposition> GetPosition()
		{
			GeolocationAccessStatus accessStatus = await Geolocator.RequestAccessAsync();

			if (accessStatus != GeolocationAccessStatus.Allowed)
				return null;

			var geoloc = new Geolocator() { DesiredAccuracyInMeters = 0 };
			return await geoloc.GetGeopositionAsync();
		}
	}
}
