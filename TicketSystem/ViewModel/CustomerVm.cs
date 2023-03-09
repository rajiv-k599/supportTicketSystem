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

        public List<Customer> Customers { get; set; }

        public CustomerGroup CustomerGroup { get; set; }

        public List<CustomerGroup> CustomerGroupList { get; set; }

        public SelectList CustomerGroupOption() => new SelectList(CustomerGroupList, nameof(CustomerGroup.Id), nameof(CustomerGroup.Name));

        public List<CustomerContact> CustomersContactList { get; set; }

        public CustomerContactVm contactVm { get; set; }
    }
}
