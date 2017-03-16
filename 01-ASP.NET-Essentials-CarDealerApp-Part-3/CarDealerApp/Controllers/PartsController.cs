namespace CarDealerApp.Controllers
{
    using CarDealer.Data;
    using CarDealer.Models;
    using Models.BindingModels;
    using Services;
    using System.Data.Entity;
    using System.Web.Mvc;

    [RoutePrefix("Parts")]
    public class PartsController : Controller
    {
        private CarDealerContext db = new CarDealerContext();
        private PartsService service = new PartsService();

        [Route]
        [Route("All")]
        public ActionResult Index()
        {
            var partsVms = service.GetAllParts();
            return View(partsVms);
        }

        [HttpGet]
        [Route("Add")]
        public ActionResult Add()
        {
            var supplierVms = service.GetAllSuppliers();
            return View(supplierVms);
        }

        [HttpPost]
        [Route("Add")]
        public ActionResult Add([Bind(Include = "SupplierId,Name,Price,Quantity")] NewPartBindingModel npbm)
        {
            Part part = service.AddNewPart(npbm);
            part.Supplier = db.Suppliers.Find(npbm.SupplierId);

            if (ModelState.IsValid)
            {
                db.Parts.Add(part);
                db.SaveChanges();
                return RedirectToAction("Index", "Parts");
            }

            return RedirectToAction("Add", "Parts");
        }

        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            var partToDeleteVm = service.GetPartToDelete(id);
            return View(partToDeleteVm);
        }

        [HttpPost]
        [Route("Delete/{id}")]
        public ActionResult Delete([Bind(Include = "Id")] DeletePartBindingModel dpbm)
        {
            service.DeletePart(dpbm.Id);
            return RedirectToAction("Index", "Parts");
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            var partToEditVm = service.DisplayPartToEdit(id);
            return View(partToEditVm);
        }

        [HttpPost]
        [Route("Edit/{id}")]
        public ActionResult Edit([Bind(Include = "Id, Price, Quantity")] EditPartBindingModel epbm)
        {
            // Cannot use service because we are using diffrent contexts?
            Part editedPart = service.GetEditedPart(epbm, db);

            if (ModelState.IsValid)
            {
                this.db.Entry(editedPart).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Parts/All");
            }

            return RedirectToAction("Edit", "Parts", new {id = epbm.Id});
        }
    }
}