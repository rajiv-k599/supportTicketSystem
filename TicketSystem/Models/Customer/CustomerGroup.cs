using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Customer
{
    public class CustomerGroup
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }    
    }
}
