using Heroicrud.Models;
using Microsoft.EntityFrameworkCore;

namespace Heroicrud.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Heroi> Herois { get; set; }
        public DbSet<SuperPoder> SuperPoder { get; set; }
        public DbSet<HeroiSuperPoderes> HeroiSuperPoderes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=DESKTOP-HEBMCIL;Initial Catalog=Heroi;Integrated Security=True;Encrypt=True;Trust Server Certificate=True");
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);

            base.OnConfiguring(optionsBuilder);
        }
    }
}
