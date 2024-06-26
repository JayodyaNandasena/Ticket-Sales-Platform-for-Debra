using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Data
{
    public class AppDBContext : DbContext
    {
        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerAccount> PartnerAccouts { get; set; }


        //no need to do as public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) because only one DBContext is available for this project
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

    }
}
