using System.Linq;

namespace Route.DAL
{
    public static class DbInitializer
    {
        public static void Initialize(RouteAppContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}