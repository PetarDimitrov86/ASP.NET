namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using CarDealer.Data;
    using Services;
    using Models.BindingModels;
    using Models.ViewModels;
    using System.Linq;

    [RoutePrefix("Logs")]
    public class LogsController : Controller
    {
        private CarDealerContext db;
        private LogsService service;

        public LogsController()
        {
            this.service = new LogsService();
            this.db = new CarDealerContext();
        }

        [Route("Index/{page=1}")]
        public ActionResult Index(int page)
        {
            var logsVms = service.GetAllLogs(page);
            return View(logsVms);
        }

        [Route("Delete/{id}")]
        public ActionResult Delete(int id)
        {
            DeleteLogViewModel delLog = service.GetLogToDeleteInfo(id);
            return View(delLog);
        }

        [Route("Delete/{id}")]
        [HttpPost]
        public ActionResult Delete([Bind(Include = "Id")] DeleteLogBindingModel dlbm)
        {
            var LogToRemove = db.Logs.Find(dlbm.Id);
            db.Logs.Remove(LogToRemove);
            db.SaveChanges();
            return RedirectToAction("Index", "Logs", new {page = 1});
        }

        [Route("Find/{username}")]
        public ActionResult Find(string username)
        {
            if (db.Users.Any(u => u.Username.Contains(username)))
            {
                var userLogVms = service.GetFilteredLogs(username);
                return this.View(userLogVms);
            }

            return RedirectToAction("Index" ,"Logs", new { page = 1});
        }

        [Route("Find")]
        [HttpPost]
        public ActionResult Find([Bind(Include = "Username")] FindUserLogsBindingModel fulbm)
        {
            return RedirectToAction($"Find/{fulbm.Username}", "Logs");
        }

        [Route("ClearAll")]
        public ActionResult ClearAll()
        {
            db.Database.ExecuteSqlCommand("delete from Logs");
            return RedirectToAction("Index", "Home");
        }

        [Route("UsersInfo")]
        public ActionResult UsersInfo()
        {
            var personsVm = service.GetAllUsersInfo();
            return View(personsVm);
        }
    }
}