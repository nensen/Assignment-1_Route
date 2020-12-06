using System;

namespace Route.Business
{
    public class RouteCalculator : IRouteCalculator
    {
        public double GetRouteFuelConsumption(double constumptionPer100, double distance)
        {
            var distanceInKm = distance / 1000;
            var consumption = (distanceInKm / 100) * constumptionPer100;
            return Math.Round(consumption, 2);
        }
    }
}