using Microsoft.VisualStudio.TestTools.UnitTesting;
using Route.Business;

namespace Route.Test
{
    [TestClass]
    public class FuelConsumption
    {
        private readonly IRouteCalculator routeCalculator;

        public FuelConsumption()
        {
            routeCalculator = new RouteCalculator();
        }

        [TestMethod]
        [DataRow(1, 1000, 0.01)]
        [DataRow(5, 500000, 25)]
        [DataRow(6, 100000, 6)]
        [DataRow(0, 0, 0)]
        public void ShouldCalulateCorrectFuelConsumption(double consumptionPer100, double distance, double correctResult)
        {
            var result = routeCalculator.GetRouteFuelConsumption(consumptionPer100, distance);

            Assert.AreEqual(result, correctResult);
        }
    }
}