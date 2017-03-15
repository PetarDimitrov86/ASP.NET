namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Linq;
    using System.Web.Mvc;
    using Services;
    using Models.BindingModels;

    [RoutePrefix("Sales")]
    public class SalesController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private SalesService service = new SalesService();

        public ActionResult Index()
        {
            return View(db.Sales.ToList());
        }

        [Route("{id}/")]
        public ActionResult Details(int? id)
        {
            Sale sale = db.Sales.Find(id);
            return View(sale);
        }

        [Route("Discounted/{percent?}/")]
        public ActionResult Discounted(int? percent)
        {
            if (percent == 0 || percent == null)
            {
                return View(db.Sales.Where(s => s.Discount != 0).ToList());
            }
            return View(db.Sales.Where(s => s.Discount * 100 == percent && s.Discount != 0).ToList());
        }

        [Route("Add")]
        public ActionResult Add()
        {
            var saleVm = service.GetAddSaleDetails();
            return View(saleVm);
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add([Bind(Include = "Customer,Car,Discount")] AddSaleBindingModel asbm)
        {
            if (db.Cars.Find(asbm.Car) != null && db.Customers.Find(asbm.Customer) != null)
            {
                return RedirectToAction("Review", new { Customer = asbm.Customer, Car = asbm.Car, Discount = asbm.Discount });
            }
            return View(asbm);
        }

        [Route("Review")]
        public ActionResult Review(AddSaleBindingModel asbm)
        {
            var reviewSaleVM = service.ReviewFinalOffer(asbm);
            return View(reviewSaleVM);
        }

        [Route("Review")]
        [HttpPost]
        public ActionResult Review([Bind(Include = "CustomerId,CarId,Discount")] ReviewSaleBindingModel rsbm)
        {
            Sale sale = service.FinalizeSale(rsbm, db);
            if (ModelState.IsValid)
            {
                db.Sales.Add(sale);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rsbm);
        }
    }
}