using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Ticket
{
    public class Ticket
    {
        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }
        public string Description { get; set; }

        public char Status { get; set; }
        
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set;}
        public DateTime? UpdatedOn { get; set;}

        public virtual TicketGroup TicketGroup { get; set; }
        public long TicketGroupId { get; set; }


    }
}
