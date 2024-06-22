using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<AdminAccount> AdminAccounts { get; set; }


        //no need to do as public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) because only one DBContext is available for this project
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

    }
}
