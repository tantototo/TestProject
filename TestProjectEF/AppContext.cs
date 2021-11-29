using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace TestProjectEF
{
    internal class AppContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        //public DbSet<History> Histories { get; set; } = null!;

        public AppContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            var config = builder.Build();
            string connectionString = config.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>()
            .HasOne(a => a.Person)
            .WithMany(p => p.Accounts)
            .HasForeignKey(a => a.PersonId);
        }
    }
}
