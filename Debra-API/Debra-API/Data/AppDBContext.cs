using Debra_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Debra_API.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<AdminAccount> AdminAccounts { get; set; }
        public DbSet<Customer> Customers { get; set; }  
        public DbSet<Partner> Partners { get; set; }
        public DbSet<PartnerAccount> PartnerAccounts { get; set; }
        public DbSet<TicketDetails> TicketDetails { get; set; }
        public DbSet<Band> Bands { get; set; }
        public DbSet<Musician> Musicians { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TicketDetails>().Property(p => p.UnitPrice).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<TicketDetails>().Property(p => p.CommisionPerTicket).HasColumnType("decimal(18,2)");
			modelBuilder.Entity<Event>().Property(p => p.Image).HasColumnType("VarBinary(max)");
			modelBuilder.Entity<Musician>().Property(p => p.Image).HasColumnType("VarBinary(max)");
			modelBuilder.Entity<Band>().Property(p => p.Image).HasColumnType("VarBinary(max)");


			//one-to-many Relationships
			modelBuilder.Entity<Partner>()
                .HasMany(e => e.Events)
                .WithOne(e => e.Partner)
                .HasForeignKey(e => e.PartnerId)
                .IsRequired();

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Event)
                .HasForeignKey(e => e.EventId)
                .IsRequired();

            modelBuilder.Entity<TicketDetails>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.TicketDetails)
                .HasForeignKey(e => e.DetailsId)
                .IsRequired();

            modelBuilder.Entity<Customer>()
                .HasMany(e => e.Tickets)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired(false);

            modelBuilder.Entity<Event>()
                .HasMany(e => e.Musicians)
                .WithOne(e => e.Event)
				.HasForeignKey(e => e.EventId)
				.IsRequired();

			modelBuilder.Entity<Event>()
                .HasMany(e => e.Bands)
                .WithOne(e => e.Event)
				.HasForeignKey(e => e.EventId)
				.IsRequired();


		}
        
    }
}
