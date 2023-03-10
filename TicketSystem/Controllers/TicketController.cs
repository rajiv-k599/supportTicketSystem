using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Constants;
using TicketSystem.Data;
using TicketSystem.Models.Customer;
using TicketSystem.Models.Ticket;
using TicketSystem.ViewModel;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TicketSystem.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly INotyfService _notyfService;
        private readonly IWebHostEnvironment WebHostEnvironment;

        public TicketController(INotyfService notyfService, AppDbContext dbContext, IWebHostEnvironment webHostEnvironment)
        {
            _dbContext = dbContext;
            _notyfService = notyfService;
            WebHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var data = new TicketVm();
            data.TicketGroupList = _dbContext.TicketGroups.Where(X => X.Status == Status.Active).ToList();
            data.TicketLists = _dbContext.Tickets.Where(X => X.Status == Status.Open).Include("TicketGroup").ToList();

            return View(data);
        }
        public IActionResult Create()
        {
            var data = new TicketVm();
            data.TicketGroupList = _dbContext.TicketGroups.Where(X => X.Status == Status.Active).ToList();
            return View(data);
            return View();
        }
        [HttpPost]
        public IActionResult Create(TicketVm ticketVm)
        {
            try
            {
                var imagePath = upload(ticketVm);
                var data = new Ticket()
                {
                    Title = ticketVm.Title,
                    Description = ticketVm.Description,
                    CreatedBy= ticketVm.CreatedBy,
                    CreatedOn = DateTime.Now,
                    TicketGroupId = ticketVm.TicketGroupId,
                    Image = imagePath,
                    Status = Status.Open
                };
                _dbContext.Tickets.Add(data);
                _dbContext.SaveChanges();
                _notyfService.Success("Created Sucessfully");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }

            return RedirectToAction(nameof(Index));
        }

        private string upload(TicketVm vm)
        {
            string fileName = "";
            if (vm.Image != null)
            {
                string uploadDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images");
                fileName = Guid.NewGuid().ToString() + "-" + vm.Image.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    vm.Image.CopyTo(fileStream);
                }
            }
            return fileName;
        }
        public IActionResult Edit(long id)
        {
            var customer = _dbContext.Tickets.Where(x => x.Id == id && x.Status == Status.Open).FirstOrDefault();
            var data = new TicketVm()
            {
                Title = customer.Title,
                Description = customer.Description,
                CreatedBy = customer.CreatedBy,             
                TicketGroupId = customer.TicketGroupId,
            };
            data.TicketGroupList = _dbContext.TicketGroups.Where(X => X.Status == Status.Active).ToList();

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(TicketVm ticketVm)
        {
            try
            {
                var data = _dbContext.Tickets.Find(ticketVm.Id);
                data.Title = ticketVm.Title;
                data.Description = ticketVm.Description;
                data.CreatedBy = ticketVm.CreatedBy;
                data.UpdatedOn = DateTime.Now;
                if (ticketVm.Image != null)
                {
                    string delDir = Path.Combine(WebHostEnvironment.WebRootPath, "Images", data.Image);
                    FileInfo f1 = new FileInfo(delDir);
                    if (f1.Exists)
                    {
                        System.IO.File.Delete(delDir);
                        f1.Delete();
                    }
                    string imagePath = upload(ticketVm);
                    data.Image = imagePath;
                }
                    _dbContext.Tickets.Update(data);
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
                var data = _dbContext.Tickets.Find(id);
                if (data != null)
                {
                    data.Status = Status.Closed;
                }
                _dbContext.Tickets.Update(data);
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
