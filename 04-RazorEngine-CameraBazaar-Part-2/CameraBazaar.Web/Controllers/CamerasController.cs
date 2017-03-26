namespace CameraBazaar.Web.Controllers
{
    using System.Data.Entity;
    using System.Linq;
    using System.Web.Mvc;
    using Data;
    using Models.BindingModels.Camera;
    using Models.EntityModels;
    using Models.ViewModels.Camera;
    using Services;
    using Microsoft.AspNet.Identity;

    [RoutePrefix("Cameras")]
    public class CamerasController : Controller
    {
        private CamerasService service;
        private CameraBazaarContext db;

        public CamerasController()
        {
            this.db = new CameraBazaarContext();
            this.service = new CamerasService(db);
        }

        [Route("All")]
        public ActionResult All()
        {
            var camEntities = db.Cameras.ToList();
            var camVms = service.GetAllCams(camEntities);
            return View(camVms);
        }

        [Route("Add")]
        [Authorize(Roles = "Seller")]
        public ActionResult Add()
        {
            return View();
        }

        [Route("Add")]
        [HttpPost]
        [Authorize(Roles = "Seller")]
        public ActionResult Add([Bind(Exclude = "")] AddCameraBindingModel acbm)
        {
            var camera = service.CreateCameraEntity(acbm, db);
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);
            camera.Seller = currentSeller;

            if (ModelState.IsValid)
            {
                db.Cameras.Add(camera);
                db.SaveChanges();
                return RedirectToAction("All", "Cameras");
            }

            return View();
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            var camDetailsVm = service.CameraDetails(id);
            return View(camDetailsVm);
        }

        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);
            if (currentSeller != null && currentSeller.Cameras.All(c => c.Id != id))
            {
                return RedirectToAction("Details", "Sellers", new { id = currentSeller.Id });
            }

            DeleteCameraViewModel delCamVm = service.GetCamDeleteInfo(id);
            return View(delCamVm);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public ActionResult Delete([Bind(Exclude = "")] DeleteCameraBindingModel dcbm)
        {
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);
            if (currentSeller.Cameras.All(c => c.Id != dcbm.CameraId))
            {
                return RedirectToAction("Details", "Sellers", new { id = currentSeller.Id });
            }

            Camera camToRemove = db.Cameras.Find(dcbm.CameraId);
            db.Cameras.Remove(camToRemove);
            db.SaveChanges();
            return RedirectToAction("Details", "Sellers", new { id = currentSeller.Id });
        }

        [Route("Edit/{id}")]
        [Authorize(Roles = "Seller")]
        public ActionResult Edit(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);
            if (currentSeller.Cameras.All(c => c.Id != id))
            {
                return RedirectToAction("Details", "Sellers", new { id = currentSeller.Id });
            }

            EditCameraViewModel editCamVm = service.GetCamEditInfo(id);
            return View(editCamVm);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        [Authorize(Roles = "Seller")]
        public ActionResult Edit([Bind(Exclude = "")] EditCameraBindingModel ecbm)
        {
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);
            if (currentSeller.Cameras.All(c => c.Id != ecbm.Id))
            {
                return RedirectToAction("Details", "Sellers", new { id = currentSeller.Id });
            }

            Camera camToEdit = db.Cameras.Find(ecbm.Id);
            camToEdit.Description = ecbm.Description;
            camToEdit.Price = ecbm.Price;
            camToEdit.Quantity = ecbm.Quantity;
            if (ModelState.IsValid)
            {
                db.Entry(camToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Sellers", new { id = currentSeller.Id });
            }

            EditCameraViewModel editCamVm = service.GetCamEditInfo(ecbm.Id);
            return View(editCamVm);
        }
    }
}