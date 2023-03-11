using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketSystem.Constants;
using TicketSystem.Data;
using TicketSystem.Models;

namespace TicketSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger,INotyfService notyfService, AppDbContext dbContext)
        {
            _logger = logger;
            _notyfService = notyfService;
            _dbContext= dbContext;
        }

        public IActionResult Index()
        {
            var totalCustomer = _dbContext.Customers.Where(x => x.Status == Status.Active).ToList();
            var openTicket = _dbContext.Tickets.Where(x => x.Status == Status.Open).ToList();
            var closedTicket = _dbContext.Tickets.Where(x => x.Status == Status.Closed).ToList();
            ViewBag.TotalCustomer = totalCustomer.Count;
            ViewBag.OpenTicket = openTicket.Count;
            ViewBag.ClosedTicket = closedTicket.Count;
            return View();
        }

        public IActionResult Privacy()
        {
            _notyfService.Success("Hello");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}