using Microsoft.EntityFrameworkCore;
using TicketSystem.Models.Customer;
using TicketSystem.Models.Ticket;

namespace TicketSystem.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
        public DbSet<CustomerGroup> CustomerGroups { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerContact> CustomerContacts { get; set; }

        public DbSet<TicketGroup> TicketGroups { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
