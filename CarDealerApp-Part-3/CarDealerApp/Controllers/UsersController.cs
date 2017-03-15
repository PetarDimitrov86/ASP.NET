namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using CarDealer.Models;
    using CarDealer.Data;

    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        private CarDealerContext db;

        public UsersController()
        {
            this.db = new CarDealerContext();
        }

        [Route("Register")]
        public ActionResult Register()
        {
            return View();
        }

        [Route("Register")]
        [HttpPost]
        public ActionResult Register([Bind(Exclude = "")] RegisterUserBindingModel rubm)
        {
            User user = new User
            {
                Email = rubm.Email,
                Username = rubm.Username,
                Password = rubm.Password
            };

            if (ModelState.IsValid && rubm.Password == rubm.ConfirmPassword)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login");
            }

            return View();
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }
    }
}