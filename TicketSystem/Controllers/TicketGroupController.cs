using AspNetCoreHero.ToastNotification.Abstractions;
using AspNetCoreHero.ToastNotification.Notyf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Constants;
using TicketSystem.Data;
using TicketSystem.Models.Customer;
using TicketSystem.Models.Ticket;
using TicketSystem.ViewModel;

namespace TicketSystem.Controllers
{
    public class TicketGroupController : Controller
    {
        private readonly INotyfService _notyfService;
        private readonly AppDbContext _dbContext;

        public TicketGroupController(INotyfService notyfService, AppDbContext dbContext) 
        {
            _notyfService = notyfService;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var group = new TicketGroupVm();
            group.TicketGroupLists = _dbContext.TicketGroups.Where(x => x.Status == Status.Active).ToList();
            return View(group);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(TicketGroupVm groupVm)
        {
            try
            {
                var group = new TicketGroup
                {
                    Name = groupVm.Name,
                    Description = groupVm.Description,
                    Status = Status.Active,
                };
                _dbContext.TicketGroups.Add(group);
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
            var group = _dbContext.TicketGroups.Where(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
            var data = new TicketGroupVm()
            {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
            };

            return View(data);
        }

        [HttpPost]
        public IActionResult Edit(TicketGroupVm groupVm)
        {
            try
            {
                var data = _dbContext.TicketGroups.Find(groupVm.Id);
                if (data != null)
                {
                    data.Name = groupVm.Name;
                    data.Description = groupVm.Description;

                }
                _dbContext.TicketGroups.Update(data);
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
                var data = _dbContext.TicketGroups.Find(id);
                var Exist = _dbContext.Tickets.Where(x => x.TicketGroupId == id && x.Status == Status.Open).ToList();
                if (Exist.Count > 0) { throw new Exception("In use cannot be deleted"); }
                if (data != null)
                {
                    data.Status = Status.Inactive;
                }
                _dbContext.TicketGroups.Update(data);
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
