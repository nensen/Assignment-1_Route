using Microsoft.EntityFrameworkCore;
using Route.Models.Data;

namespace Route.DAL
{
    public class RouteAppContext : DbContext
    {
        public RouteAppContext(DbContextOptions<RouteAppContext> options) : base(options)
        {
        }

        public DbSet<RouteInfo> RouteInfos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RouteInfo>().ToTable("RouteInfo");
        }
    }
}