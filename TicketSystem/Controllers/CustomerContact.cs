using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.Controllers
{
    public class CustomerContact : Controller
    {
        public CustomerContact() 
        { 
        
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
