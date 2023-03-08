using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models.Customer
{
    public class CustomerContact
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Designation { get; set; }
        public string PhoneNumber { get; set; }
        public string MobileNumber { get; set; }
        public char Status { get; set; }
    }
}
