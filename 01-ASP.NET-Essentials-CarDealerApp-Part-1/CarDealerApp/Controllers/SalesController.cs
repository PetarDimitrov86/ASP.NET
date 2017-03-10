namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Linq;
    using System.Web.Mvc;

    public class SalesController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        // GET: Sales
        public ActionResult Index()
        {
            return View(db.Sales.ToList());
        }
        public ActionResult Details(int? id)
        {
            Sale sale = db.Sales.Find(id);
            return View(sale);
        }

        public ActionResult Discounted(int? percent)
        {
            if (percent == 0 || percent == null)
            {
                return View(db.Sales.Where(s => s.Discount != 0).ToList());
            }
            return View(db.Sales.Where(s => s.Discount * 100 == percent && s.Discount != 0).ToList());
        }
    }
}