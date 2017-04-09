namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services;

    public class HomeController : Controller
    {
        private CoursesService service;

        public HomeController()
        {
            this.service = new CoursesService();
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