using Microsoft.AspNetCore.Mvc.Rendering;
using TicketSystem.Models.Customer;
using TicketSystem.Models.Ticket;

namespace TicketSystem.ViewModel
{
    public class TicketVm
    {
        public long Id { get; set; }

        public string Title { get; set; }

        public IFormFile Image { get; set; }
        public string Description { get; set; }

        public char Status { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public long TicketGroupId { get; set; }

        public List<Ticket> TicketLists { get; set;}

        public TicketGroup TicketGroup { get; set; }

        public List<TicketGroup> TicketGroupList { get; set; }

        public SelectList TicketGroupOption() => new SelectList(TicketGroupList, nameof(TicketGroup.Id), nameof(TicketGroup.Name));

        public CommentVm CommentVm { get; set; }
        public List<Comment> CommentLists { get; set; } 
    }
}
