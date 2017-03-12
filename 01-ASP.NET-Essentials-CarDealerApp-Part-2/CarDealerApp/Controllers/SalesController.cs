namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Linq;
    using System.Web.Mvc;

    [RoutePrefix("Sales")]
    public class SalesController : Controller
    {
        private CarDealerContext db = new CarDealerContext();

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
    }
}