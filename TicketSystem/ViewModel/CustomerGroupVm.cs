using System.ComponentModel.DataAnnotations;
using TicketSystem.Models.Customer;

namespace TicketSystem.ViewModel
{
    public class CustomerGroupVm
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "please enter name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "please enter Description")]
        public string Description { get; set; }

        public List<CustomerGroup> customerGroups { get; set; }

    }
}
