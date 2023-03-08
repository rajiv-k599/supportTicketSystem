using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Data;
using TicketSystem.Models.Customer;
using TicketSystem.ViewModel;
using TicketSystem.Constants;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace TicketSystem.Controllers
{
    public class CustomerGroupController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _dbContext;

        public CustomerGroupController(INotyfService notyfService, AppDbContext dbContext)
        {
            _notyfService = notyfService;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var group = new CustomerGroupVm();
            group.customerGroups = _dbContext.CustomerGroups.Where(x => x.Status == Status.Active).ToList();
            return View(group);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CustomerGroupVm groupVm)
        {
            try
            {
                var group = new CustomerGroup
                {
                    Name = groupVm.Name,
                    Description = groupVm.Description,
                    Status = Status.Active,
                };
                _dbContext.CustomerGroups.Add(group);
                _dbContext.SaveChanges();
                _notyfService.Success("Create sucessfully");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(long id)
        {
            var group = _dbContext.CustomerGroups.Where(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
            var data = new CustomerGroupVm()
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
            };

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(CustomerGroupVm groupVm)
        {
            try
            {
                var data = _dbContext.CustomerGroups.Find(groupVm.Id);
                if (data != null)
                {
                    data.Name = groupVm.Name;
                    data.Description = groupVm.Description;

                }
                _dbContext.CustomerGroups.Update(data);
                _dbContext.SaveChanges();
                _notyfService.Success("updated sucessfully");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(long id)
        {
            try
            {
                var data = _dbContext.CustomerGroups.Find(id);
                if (data != null)
                {
                    data.Status = Status.Inactive;
                }
                _dbContext.CustomerGroups.Update(data);
                _dbContext.SaveChanges();

                _notyfService.Success("Deleted Sucessfully");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
