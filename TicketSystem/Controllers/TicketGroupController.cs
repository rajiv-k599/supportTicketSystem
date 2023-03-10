using Microsoft.AspNetCore.Mvc;

namespace TicketSystem.Controllers
{
    public class TicketGroupController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
