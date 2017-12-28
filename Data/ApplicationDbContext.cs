using Microsoft.EntityFrameworkCore;
using statusservice.Model;

namespace statusservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ThingStatus> ActiveThingStatus { get; set; }
        public DbSet<ContextStatus> ContextStatus { get; set; }
        public DbSet<HistoryContextStatus> HistoryContextStatus { get; set; }
        public DbSet<HistoryThingStatus> HistoryThingStatus { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}