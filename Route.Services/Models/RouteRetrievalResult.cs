using Route.Models;

namespace Route.Services.Models
{
    public class RouteRetrievalResult
    {
        public bool Success { get; set; }

        public string ErrorMessage { get; set; }

        public TextValueWrapper TotalDistance { get; set; }

        public TextValueWrapper TotalDuration { get; set; }
    }
}