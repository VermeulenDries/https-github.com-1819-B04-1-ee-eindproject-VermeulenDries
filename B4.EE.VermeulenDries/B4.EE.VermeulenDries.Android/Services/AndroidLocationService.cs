using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Essentials;
using B4.EE.VermeulenDries.Services;
using Xamarin.Forms;

[assembly:Dependency(typeof(B4.EE.VermeulenDries.Droid.Services.AndroidLocationService))]
namespace B4.EE.VermeulenDries.Droid.Services
{
    public class AndroidLocationService: ILocationService
    {
        public string coords { get; set; }
        public async void GetLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    coords = $"{location.Latitude}, {location.Longitude}";
                }
            }

            catch(Exception e)
            {
                coords="Location not found";
            }
        }

        public string GetCoords()
        {
            return coords;
        }
    }
}