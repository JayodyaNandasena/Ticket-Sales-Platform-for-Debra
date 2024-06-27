using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;

namespace Debra_API.Data
{
    public class AppDBContext : DbContext
    {
        //no need to do as public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) because only one DBContext is available for this project
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerAccount> PartnerAccouts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Category>().Property(p => p.CommisionPerTicket).HasColumnType("decimal(18,2)");

            //one-to-many Relationships (with shadow foreign keys)
            modelBuilder.Entity<Partner>()
                .HasMany(e => e.Events)
                .WithOne(e => e.Partner)
                .HasForeignKey("PartnerId")
                .IsRequired();

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Event)
                .HasForeignKey("EventId")
                .IsRequired();

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Category)
                .HasForeignKey("CategoryId")
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Customer)
                .HasForeignKey("CustomerId")
                .IsRequired(false);

            //many-to-many Relationships
            modelBuilder.Entity<Event>()
                .HasMany(e => e.Musicians)
                .WithMany(e => e.Events)
                .UsingEntity("EventMusicians");

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Bands)
                .WithMany(e => e.Events)
                .UsingEntity("EventBands");


        }
        
    }
}
