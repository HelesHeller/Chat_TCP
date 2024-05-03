using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Server_TCP
{
      class ApplicationContext : DbContext
    {
        public  DbSet<User> Users { get; set; }
        public DbSet<Log> Logs { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=HELES; Database=TESTSERVER; Trusted_Connection=True; TrustServerCertificate=True; Integrated Security=True;");
            base.OnConfiguring(optionsBuilder);
        }
    }
}
