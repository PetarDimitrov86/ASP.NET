namespace CarDealerApp.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using CarDealer.Models;
    using CarDealer.Data;
    using System.Linq;
    using Security;

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

        [Route("Login")]
        [HttpPost]
        public ActionResult Login([Bind(Include = "Username, Password")] LoginUserBindingModel lubm)
        {
            User user = db.Users.FirstOrDefault(u => u.Username == lubm.Username && u.Password == lubm.Password);

            if (user != null)
            {
                Session sessionToCheck =
                    db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID && s.User.Id == user.Id);

                if (sessionToCheck != null)
                {
                    sessionToCheck.IsActive = true;
                }
                else
                {
                    Session session = new Session
                    {
                        IsActive = true,
                        SessionId = this.Session.SessionID,
                        User = user
                    };
                    db.Sessions.Add(session);
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            SignInManager.Logout(this.Session.SessionID);
            return RedirectToAction("Index", "Home");
        }
    }
}