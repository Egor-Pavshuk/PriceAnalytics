using Microsoft.EntityFrameworkCore;
using PriceAnalytics.Core.DbModels;

namespace PriceAnalytics.DAL
{
    public class PriceAnalyticsDbContext : DbContext
    {
        public PriceAnalyticsDbContext(DbContextOptions options) : base(options) { }

        public DbSet<SaleApplication> SaleApplications { get; set; }
        public DbSet<DailyResult> DailyResults { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
