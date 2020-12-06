using Route.Services.Models;
using System.Threading.Tasks;

namespace Route.Services
{
    public interface IRouteService
    {
        Task<RouteRetrievalResult> GetRouteInfoFromGoogle(string initialDestination, string sourceDestination);

        Task<CoordinateRetrievalResult> GetCoordinateFromAddress(string address);
    }
}