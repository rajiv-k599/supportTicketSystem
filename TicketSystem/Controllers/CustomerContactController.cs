using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.Controllers
{
    public class CustomerContactController : Controller
    {
        public CustomerContactController() 
        { 
        
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
