namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Models;
    using Services;
    using Models.BindingModels;

    [RoutePrefix("Cars")]
    public class CarsController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private CarsService service = new CarsService();

        // GET: Cars
        [Route("{make=}")]
        public ActionResult Index(string make)
        {
            IEnumerable<Car> carsToDisplay = db.Cars.Where(c => c.Make.Contains(make)).OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance);

            return View(carsToDisplay);
        }

        [Route("Find")]
        public ActionResult Find([Bind(Include = "Make")] FindCarsBindingModel fcbm)
        {
            return RedirectToAction($"{fcbm.Make}", "Cars");
        }

        [Route(@"{id:min(1):max(2000)?}/Parts")]
        //[Route(@"{id:regex(\d+)?}/Parts")]
        public ActionResult Parts(int? id)
        {
            Car carToDisplay = db.Cars.Find(id);
            return View(carToDisplay);
        }

        [Route("Add")]
        public ActionResult Add()
        {
            var partVMs = service.GetPartsForDropdown();
            return this.View(partVMs);
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance, Part1, Part2, Part3")] NewCarBindingModel ncbm)
        {
            Car newCar = service.AddNewCar(ncbm, db);

            if (ModelState.IsValid)
            {
                db.Cars.Add(newCar);
                db.SaveChanges();
                this.service.GenerateLog(ModifiedTable.Car, Operation.Add, db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User.Id);
                return RedirectToAction("Index", "Cars");
            }

            return RedirectToAction("Add", "Cars");
        }
    }
}