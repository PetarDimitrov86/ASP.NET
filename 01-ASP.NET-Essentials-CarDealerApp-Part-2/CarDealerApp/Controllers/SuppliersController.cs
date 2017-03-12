namespace CarDealerApp.Controllers
{
    using System.Linq;
    using System.Web.Mvc;
    using CarDealer.Data;
    using CarDealer.Models;
    using System.Collections.Generic;
    
    [RoutePrefix("Suppliers")]
    public class SuppliersController : Controller
    {
        private CarDealerContext db = new CarDealerContext();

        // GET: Suppliers
        [Route("{type}")]
        public ActionResult Index(string type)
        {
            IEnumerable<Supplier> suppliers = null;
            if (type.ToLower() == "local")
            {
                suppliers = db.Suppliers.Where(s => !s.IsImporter);
            }
            else if (type.ToLower() == "importer")
            {
                suppliers = db.Suppliers.Where(s => s.IsImporter);
            }
            else
            {
                suppliers = db.Suppliers.ToList();
            }
            
            return View(suppliers);
        }
    }
}
