using TicketSystem.Models.Ticket;

namespace TicketSystem.ViewModel
{
    public class CommentVm
    {
        public long Id { get; set; }

        public string Content { get; set; }

        public string CommentedBy { get; set; }

        public DateTime? CommentedOn { get; set; }

        public char Status { get; set; }

        public long TicketId { get; set; }
    }
}
