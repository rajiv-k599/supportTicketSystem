using TicketSystem.Models.Ticket;

namespace TicketSystem.ViewModel
{
    public class TicketGroupVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public char Status { get; set; }

        public List<TicketGroup> TicketGroupLists { get; set; }
    }
}
