namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;

    public class HomeController : Controller
    {
        private ICoursesService service;

        public HomeController(ICoursesService service)
        {
            this.service = service;
        }

        public ActionResult Index()
        {
            var coursesVms = service.GetAllCourses("");
            return View(coursesVms);
        }

        [HttpPost]
        public ActionResult Search(string courseName = "")
        {
            var searchedCoursesVms = service.GetAllCourses(courseName);
            return this.View(searchedCoursesVms);
        }
    }
}