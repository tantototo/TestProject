using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestProjectWebApi.Models;

namespace TestProjectWebApi.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<Account> Accounts { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;

        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region AutoInclude
            modelBuilder.Entity<Person>().Navigation(e => e.Accounts).AutoInclude();
            modelBuilder.Entity<Account>().Navigation(e => e.Person).AutoInclude();
            modelBuilder.Entity<Account>().Navigation(e => e.Histories).AutoInclude();
            modelBuilder.Entity<History>().Navigation(e => e.Account).AutoInclude();
            #endregion

        }

        //public DbSet<History> History { get; set; }

    }
}
