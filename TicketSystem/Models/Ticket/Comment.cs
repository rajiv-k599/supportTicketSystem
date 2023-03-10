using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Ticket
{
    public class Comment
    {
        [Key]
        public long Id { get; set; }

        public string Content { get; set; }

        public string CommentedBy { get; set; }

        public DateTime? CommentedOn { get; set; }

        public char Status { get; set; }

        public virtual Ticket Ticket { get; set; }

        public long TicketId { get; set; }
    }
}
