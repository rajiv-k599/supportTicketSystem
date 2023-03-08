using Microsoft.AspNetCore.Mvc.Rendering;
using TicketSystem.Models.Customer;

namespace TicketSystem.ViewModel
{
    public class CustomerVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Pan { get; set; }

        public char Status { get; set; }
        public long CustomerGroupId { get; set; }

        public List<Customer> customers { get; set; }

        public List<CustomerGroup> customergroups { get; set; }

        public SelectList CustomerGroupOption() => new SelectList(customergroups, nameof(CustomerGroup.Id), nameof(CustomerGroup.Name));

    }
}
