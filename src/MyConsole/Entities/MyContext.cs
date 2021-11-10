using Microsoft.EntityFrameworkCore;

namespace MyConsole.Entities
{
    public class MyContext : DbContext
    {
        public DbSet<Revenue> Revenues { get; set; }
        public DbSet<RevenueDaily> RevenueDailies { get; set; }

        public MyContext(DbContextOptions<MyContext> options) : base (options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }
    }
}