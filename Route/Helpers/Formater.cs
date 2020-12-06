using Route.Services.Models;

namespace Route.Helpers
{
    public static class Formater
    {
        public static string GetLatLngCoords(CoordinateRetrievalResult coords)
        {
            return $"Latitude: {coords.Lat}, Longitude: {coords.Lng}";
        }
    }
}