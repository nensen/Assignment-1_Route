using Route.Models;

namespace Route.Business
{
    public interface IRouteCalculator
    {
        double GetRouteFuelConsumption(double constumptionPer100, double distance);
    }
}