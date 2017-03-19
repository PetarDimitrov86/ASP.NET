using System.Data.Entity;

namespace CameraBazaar.App.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using Services;
    using Data;
    using System.Linq;
    using Models.ViewModels;
    using CameraBazaar.Models;

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
        public ActionResult Add()
        {
            return View();
        }

        [Route("Add")]
        [HttpPost]
        public ActionResult Add([Bind(Exclude = "")] AddCameraBindingModel acbm)
        {
            var camera = service.CreateCameraEntity(acbm, db);
            camera.Seller = db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User;

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
            User currentUser = db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User;
            if (currentUser.Cameras.All(c => c.Id != id))
            {
                return RedirectToAction("Details", "Users", new { id = currentUser.Id});
            }

            DeleteCameraViewModel delCamVm = service.GetCamDeleteInfo(id);
            return View(delCamVm);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public ActionResult Delete([Bind(Exclude = "")] DeleteCameraBindingModel dcbm)
        {
            User currentUser = db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User;
            if (currentUser.Cameras.All(c => c.Id != dcbm.CameraId))
            {
                return RedirectToAction("Details", "Users", new { id = currentUser.Id });
            }

            Camera camToRemove = db.Cameras.Find(dcbm.CameraId);
            db.Cameras.Remove(camToRemove);
            db.SaveChanges();
            return RedirectToAction("Details", "Users", new { id = currentUser.Id });
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            User currentUser = db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User;
            if (currentUser.Cameras.All(c => c.Id != id))
            {
                return RedirectToAction("Details", "Users", new { id = currentUser.Id });
            }

            EditCameraViewModel editCamVm = service.GetCamEditInfo(id);
            return View(editCamVm);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "")] EditCameraBindingModel ecbm)
        {
            User currentUser = db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User;
            if (currentUser.Cameras.All(c => c.Id != ecbm.Id))
            {
                return RedirectToAction("Details", "Users", new { id = currentUser.Id });
            }

            Camera camToEdit = db.Cameras.Find(ecbm.Id);
            camToEdit.Description = ecbm.Description;
            camToEdit.Price = ecbm.Price;
            camToEdit.Quantity = ecbm.Quantity;
            if (ModelState.IsValid)
            {
                db.Entry(camToEdit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Users", new {id= currentUser.Id});
            }

            EditCameraViewModel editCamVm = service.GetCamEditInfo(ecbm.Id);
            return View(editCamVm);
        }
    }
}