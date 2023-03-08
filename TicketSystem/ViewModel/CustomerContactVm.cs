using TicketSystem.Models.Customer;

namespace TicketSystem.ViewModel
{
    public class CustomerContactVm
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public char Status { get; set; }
        public long CustomerId { get; set; }
        public List<CustomerContact> Contacts { get; set; }
    }
}
