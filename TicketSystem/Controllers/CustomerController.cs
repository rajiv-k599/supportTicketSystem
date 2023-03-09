using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketSystem.Constants;
using TicketSystem.Data;
using TicketSystem.Models.Customer;
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
            data.CustomerGroupList = _dbContext.CustomerGroups.Where(X => X.Status == Status.Active).ToList();
            data.Customers = _dbContext.Customers.Where(X => X.Status == Status.Active).Include("CustomerGroup").ToList();
           
            return View(data);
        }
        public IActionResult Create() 
        {
            var data = new CustomerVm();
            data.CustomerGroupList = _dbContext.CustomerGroups.Where(X => X.Status == Status.Active).ToList();
            return View(data);
         }
        [HttpPost]
        public IActionResult Create(CustomerVm customerVm)
        {
            try
            {
                var data = new Customer()
                {
                    Name = customerVm.Name,
                    Email = customerVm.Email,
                    Phone = customerVm.Phone,
                    Pan = customerVm.Pan,
                    CustomerGroupId = customerVm.CustomerGroupId,
                    Status = Status.Active
                };
                _dbContext.Customers.Add(data);
                _dbContext.SaveChanges();
                _notyfService.Success("Created Sucessfully");
            }catch(Exception ex)
            {
                _notyfService.Error(ex.Message);
            }
           
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Details(long id)
        {
            var customer = _dbContext.Customers.Where(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
            var data = new CustomerVm()
            {
                Id = customer.Id,
                CustomerGroupId = customer.CustomerGroupId,
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                Pan = customer.Pan
            };
            data.CustomerGroup = _dbContext.CustomerGroups.Where(x => x.Id == customer.CustomerGroupId && x.Status == Status.Active).FirstOrDefault();
            data.CustomersContactList = _dbContext.CustomerContacts.Where(x => x.CustomerId == customer.Id && x.Status == Status.Active).ToList();

            return View(data);
        }

        public IActionResult Edit(long id)
        {
            var customer = _dbContext.Customers.Where(x => x.Id == id && x.Status == Status.Active).FirstOrDefault();
            var data = new CustomerVm()
            {
                Id = customer.Id,
                CustomerGroupId = customer.CustomerGroupId,
                Name = customer.Name,
                Email =   customer.Email,
                Phone = customer.Phone,
                Pan = customer.Pan
            };
            data.CustomerGroupList = _dbContext.CustomerGroups.Where(X => X.Status == Status.Active).ToList();

            return View(data);
        }
        [HttpPost]
        public IActionResult Edit(CustomerVm customerVm)
        {
            try
            {
                var data = _dbContext.Customers.Find(customerVm.Id);
                if (data != null)
                {
                    data.Name = customerVm.Name;
                    data.Email = customerVm.Email;
                    data.Phone = customerVm.Phone;
                    data.Pan = customerVm.Pan;
                    data.CustomerGroupId = customerVm.CustomerGroupId;

                }
                _dbContext.Customers.Update(data);
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
                var data = _dbContext.Customers.Find(id);
                if (data != null)
                {
                    data.Status = Status.Inactive;
                }
                _dbContext.Customers.Update(data);
                _dbContext.SaveChanges();

                _notyfService.Success("Deleted Sucessfully");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult AddContact(CustomerContactVm contactVm)
        {
            try
            {
                var data = new CustomerContact()
                {
                    Name= contactVm.Name,
                    Designation= contactVm.Designation,
                    MobileNumber= contactVm.MobileNumber,
                    PhoneNumber= contactVm.PhoneNumber,
                    CustomerId= contactVm.CustomerId,
                    Status= Status.Active
                };
                _dbContext.CustomerContacts.Add(data);
                _dbContext.SaveChanges();
                _notyfService.Success("Added Sucessfully");
            }
            catch(Exception ex)
            {
                _notyfService.Success(ex.Message);
            }
            return RedirectToAction(nameof(Details),new {id= contactVm.CustomerId });
        }
        public IActionResult RemoveContact(long id) 
        {
            var data = _dbContext.CustomerContacts.Find(id);
            try
            {
               
                if (data != null)
                {
                    data.Status = Status.Inactive;
                }
                _dbContext.CustomerContacts.Update(data);
                _dbContext.SaveChanges();

                _notyfService.Success("Deleted Sucessfully");
            }
            catch (Exception ex)
            {
                _notyfService.Error(ex.Message);
            }
            return RedirectToAction(nameof(Details),new {id = data.CustomerId});
        }

    }
}
