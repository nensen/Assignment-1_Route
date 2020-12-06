namespace Route.Models.Data
{
    public class RouteInfo
    {
        public int Id { get; set; }

        public string InitialAddress { get; set; }

        public string InitialAddressCoords { get; set; }

        public string DestinationAddress { get; set; }

        public string DestinationAddressCoords { get; set; }

        public double FuelConsumptionPer100 { get; set; }

        public string RouteFuelConsumption { get; set; }

        public string TotalDistance { get; set; }

        public string TotalDuration { get; set; }
    }
}