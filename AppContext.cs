

using Microsoft.EntityFrameworkCore;

namespace SkillFactory;

internal class AppContext : DbContext
{
    // Объекты таблицы Users
        public DbSet<UserData> Users { get; set; }
        public DbSet<Company> Companies { get; set; }
        //public DbSet<UserCredential> UserCredentials { get; set; }
        //public DbSet<Topic> Topics { get; set; }

        public AppContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(ConnectionString.GetConnectionString("127.0.0.1", "MyTest", "evgeny", "Vtcnj!21"), 
            new MySqlServerVersion(new Version(8, 0, 25)));
        }
}