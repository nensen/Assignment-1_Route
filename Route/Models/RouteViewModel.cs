using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Route.Models
{
    public class RouteViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Intial address")]
        public string InitialAddress { get; set; }

        [DisplayName("Intial address coordinates")]
        public string InitialAddressCoords { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Destination address")]
        public string DestinationAddress { get; set; }

        [DisplayName("Destination address coordinates")]
        public string DestinationAddressCoords { get; set; }

        [Required]
        [Range(0, 100000, ErrorMessage = "Fuel consumption must be between 0 and 100 000 l.")]
        [DisplayName("Fuel consumption per 100 km")]
        public double FuelConsumptionPer100 { get; set; }

        [DisplayName("Consumption")]
        public string RouteFuelConsumption { get; set; }

        [DisplayName("Distance")]
        public string TotalDistance { get; set; }

        [DisplayName("Duration")]
        public string TotalDuration { get; set; }
    }
}