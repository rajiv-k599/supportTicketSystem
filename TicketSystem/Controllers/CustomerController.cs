using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Constants;
using TicketSystem.Data;
using TicketSystem.ViewModel;

namespace TicketSystem.Controllers
{
    public class CustomerController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly INotyfService _notyfService;

        public CustomerController(INotyfService notyfService, AppDbContext dbContext)
        {
            _dbContext=dbContext;
            _notyfService = notyfService;
        }

        public IActionResult Index()
        {
            var data = new CustomerVm();
            data.customergroups = _dbContext.CustomerGroups.Where(X=> X.Status == Status.Active).ToList();
            return View(data);
        }
        public IActionResult Create() 
        {
            var data = new CustomerVm();
            data.customergroups = _dbContext.CustomerGroups.Where(X => X.Status == Status.Active).ToList();
            return View(data);
         }
        public IActionResult Details()
        {
            return View();
        }
    }
}
