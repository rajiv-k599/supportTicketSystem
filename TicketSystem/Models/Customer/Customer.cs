using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Customer
{
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pan { get; set; }

        public char Status { get; set; }

        public virtual CustomerGroup CustomerGroup { get; set; }
        public long CustomerGroupId { get; set; }

    }
}
