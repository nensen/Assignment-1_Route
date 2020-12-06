using Newtonsoft.Json;
using Route.Common;
using Route.Services.Models;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Route.Services
{
    public class RouteService : IRouteService
    {
        private const string language = "en-US";
        private const string units = "metric";

        public async Task<RouteRetrievalResult> GetRouteInfoFromGoogle(string initialDestination, string sourceDestination)
        {
            const string googleApiBaseUrl = "https://maps.googleapis.com/maps/api/distancematrix/json?mode=driving";
            var url = $"{googleApiBaseUrl}&origins={initialDestination}&destinations={sourceDestination}&language={language}&key={Constants.GoogleMatrixApiKey}&units={units}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new RouteRetrievalResult()
                    {
                        ErrorMessage = "Route retrieval failed."
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<MatrixGoogleApiResult>(responseContent);


                if (apiResult.Status != "OK")
                {
                    return new RouteRetrievalResult()
                    {
                        ErrorMessage = $"Route retrieval failed. Status: {apiResult.Status}"
                    };
                }

                var element = apiResult.Rows.First().Elements.First();

                if (element.Status != "OK")
                {
                    return new RouteRetrievalResult()
                    {
                        ErrorMessage = $"No route was found between initial and destination address."
                    };
                }

                return new RouteRetrievalResult()
                {
                    Success = true,
                    TotalDistance = element?.Distance,
                    TotalDuration = element?.Duration,
                };
            }
        }

        public async Task<CoordinateRetrievalResult> GetCoordinateFromAddress(string address)
        {
            var googleApiBaseUrl = "https://maps.googleapis.com/maps/api/geocode/json";
            var url = $"{googleApiBaseUrl}?address={address}&key={Constants.GoogleGeoLocationApiKey}";

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);

                if (!response.IsSuccessStatusCode)
                {
                    return new CoordinateRetrievalResult()
                    {
                        Success = false,
                        ErrorMessage = "Call to retrieve route failed."
                    };
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var apiResult = JsonConvert.DeserializeObject<GeoLocationGoogleApiResult>(responseContent);

                if (apiResult.Status != "OK")
                {
                    return new CoordinateRetrievalResult()
                    {
                        Success = false,
                        ErrorMessage = "No valid coordinates found for initial or destination address."
                    };
                }

                var location = apiResult.Results.FirstOrDefault()?.Geometry.Location;

                return new CoordinateRetrievalResult()
                {
                    Success = true,
                    Lat = location.Lat,
                    Lng = location.Lng
                };
            }
        }
    }
}