using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Ticket
{
    public class TicketGroup
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }
    }
}
