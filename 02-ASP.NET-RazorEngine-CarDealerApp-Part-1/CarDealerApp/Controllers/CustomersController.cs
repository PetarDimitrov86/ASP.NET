namespace CarDealerApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Collections.Generic;
    using Models.BindingModels;
    using Services;
    using Models.ViewModels;

    [RoutePrefix("Customers")]
    public class CustomersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private CustomersService service = new CustomersService();

        [Route("All/{sortType}")]
        public ActionResult Index(string sortType)
        {
            IEnumerable<Customer> orderedCustomers = null;

            if (sortType == "descending")
            {
                orderedCustomers = db.Customers.OrderByDescending(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
            }
            else if (sortType == "ascending")
            {
                orderedCustomers = db.Customers.OrderBy(c => c.BirthDate).ThenBy(c => c.IsYoungDriver);
            }
            else
            {
                orderedCustomers = db.Customers.ToList();
            }
            return View(orderedCustomers.ToList());
        }

        [Route("{id=1}")]
        public ActionResult Sales(int id)
        {
            Customer customer = db.Customers.Find(id);
            return View(customer);
        }

        [Route("Add")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult Add([Bind(Include = "Name,Date")] NewCustomerBindingModel ncbm)
        {
            Customer customer = service.CreateNewCustomer(ncbm);

            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index", "Customers", new {sortType = "all"});
            }

            return RedirectToAction("Add", "Customers");
        }

        [HttpGet]
        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            Customer customer = db.Customers.Find(id);

            EditCustomerViewModel ecvm = new EditCustomerViewModel
            {
                Date = customer.BirthDate.Value,
                Name = customer.Name,
                Id = customer.Id
            };

            return View(ecvm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult Edit([Bind (Include = "Id,Name,Date")] EditCustomerBindingModel ecbm)
        {
            Customer editedCustomer = service.EditExistingCustomer(ecbm, db);

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                return RedirectToAction("Index", "Customers", new { sortType = "all" });
            }

            return RedirectToAction("Edit", "Customers", new {id = ecbm.Id});
        }
    }
}
