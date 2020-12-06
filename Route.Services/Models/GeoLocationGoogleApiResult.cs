using System.Collections.Generic;

namespace Route.Services.Models
{
    public class GeoLocationGoogleApiResult
    {
        public List<GeoLocationApiResult> Results { get; set; }

        public string Status { get; set; }
    }

    public class GeoLocationApiResult
    {
        public string Formated_Address { get; set; }

        public Geometry Geometry { get; set; }
    }

    public class Geometry
    {
        public Location Location { get; set; }
    }

    public class Location
    {
        public string Lat { get; set; }

        public string Lng { get; set; }
    }
}