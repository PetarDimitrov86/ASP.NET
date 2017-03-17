namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Data;
    using Services;
    using Models.BindingModels;
    using CarDealer.Models;
    using Models.ViewModels;
    using System.Linq;

    [RoutePrefix("Suppliers")]
    public class SuppliersController : Controller
    {
        private CarDealerContext db;
        private SuppliersService service;

        public SuppliersController()
        {
            this.db = new CarDealerContext();
            this.service = new SuppliersService();
        }

        [Route("{type}")]
        public ActionResult Index(string type)
        {
            var suppliersVMs = service.FilterSuppliers(type);        
            return View(suppliersVMs);
        }

        [Route("Add")]
        public ActionResult Add()
        {
            return View();
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add([Bind(Include = "Importer,Name")] AddSupplierBindingModel asbm)
        {
            Supplier supplier = new Supplier
            {
                IsImporter = asbm.Importer,
                Name = asbm.Name
            };
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(supplier);
                db.SaveChanges();
                this.service.GenerateLog(ModifiedTable.Supplier, Operation.Add, db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User.Id);
                return RedirectToAction("Index", "Suppliers", new { type = "all"});
            }
            return View();
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var editSupplierVm = service.GetSupplierToEditInfo(id);
            return View(editSupplierVm);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        public ActionResult Edit([Bind(Include = "Id,IsImporter,Name")] EditSupplierBindingModel esbm)
        {
            service.EditSupplier(esbm);
            if (ModelState.IsValid)
            {
                this.service.GenerateLog(ModifiedTable.Supplier, Operation.Edit, db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User.Id);
                return RedirectToAction("Index", "Suppliers", new { type = "all" });
            }
            return RedirectToAction("Edit", "Suppliers", new { id = esbm.Id});
        }

        [Route("Delete/{id}")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            DeleteSupplierViewModel delSuplVm = service.ConfirmDeleteModel(id);
            return View(delSuplVm);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id")] DeleteSupplierBindingModel dsbm)
        {
            Supplier supplierToRemove = db.Suppliers.Find(dsbm.Id);
            if (ModelState.IsValid)
            {
                db.Suppliers.Remove(supplierToRemove);
                db.SaveChanges();
                this.service.GenerateLog(ModifiedTable.Supplier, Operation.Delete, db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User.Id);
                return RedirectToAction("Index", "Suppliers", new { type = "all" });
            }
            return RedirectToAction("Delete", "Suppliers", new { id = dsbm.Id });
        }
    }
}