namespace CarDealerApp.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Models;
    using Services;
    using Models.BindingModels;
    using System;

    [RoutePrefix("Cars")]
    public class CarsController : Controller
    {
        private CarDealerContext db;
        private CarsService service;

        public CarsController()
        {
            this.db = new CarDealerContext();
            this.service = new CarsService();
        }

        [Route("{make=}")]
        public ActionResult Index(string make)
        {
            IEnumerable<Car> carsToDisplay = db.Cars.Where(c => c.Make.Contains(make)).OrderBy(c => c.Model).ThenByDescending(c => c.TravelledDistance);

            return View(carsToDisplay);
        }

        [Route("{make=}")]
        [HttpPost]
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
        public ActionResult Add([Bind(Include = "Make, Model, TravelledDistance, Parts")] NewCarBindingModel ncbm)
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

        [Route("Details/{id}")]
        [HandleError(ExceptionType = typeof(ArgumentOutOfRangeException), View = "SadFaceError")]
        [HandleError(ExceptionType = typeof(InvalidOperationException), View = "FirstCustomError")]
        public ActionResult Details(int id)
        {
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                throw new ArgumentOutOfRangeException(null,  $"Could not find a car with ID {id}");
            }
            if (car.TravelledDistance > 1000000)
            {
                throw new InvalidOperationException("The car is too old to be displayed.");
            }
            ViewData["car"] = car;
            return View();
        }
    }
}