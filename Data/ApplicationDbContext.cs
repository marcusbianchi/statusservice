using Microsoft.EntityFrameworkCore;
using statusservice.Model;

namespace statusservice.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Status> ActiveStatus { get; set; }
        public DbSet<StatusDescription> StatusDescriptions { get; set; }
        public DbSet<Status> HistoryStatus { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }
    }
}