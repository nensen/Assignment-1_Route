namespace Route.Services.Models
{
    public class CoordinateRetrievalResult
    {
        public string Lat { get; set; }

        public string Lng { get; set; }

        public bool Success { get; set; }

        public string ErrorMessage { get; set; }
    }
}