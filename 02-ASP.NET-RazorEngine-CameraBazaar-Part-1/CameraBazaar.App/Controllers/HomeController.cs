using System.Web.Mvc;

namespace CameraBazaar.App.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}