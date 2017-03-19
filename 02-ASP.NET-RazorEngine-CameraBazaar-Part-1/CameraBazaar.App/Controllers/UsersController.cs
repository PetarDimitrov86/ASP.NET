namespace CameraBazaar.App.Controllers
{
    using System.Web.Mvc;
    using Models.BindingModels;
    using CameraBazaar.Models;
    using Data;
    using System.Linq;
    using Security;
    using Services;
    using System.Data.Entity;
    using Models.ViewModels;

    [RoutePrefix("Users")]
    public class UsersController : Controller
    {
        private CameraBazaarContext db;
        private CamerasService service;

        public UsersController()
        {
            this.db = new CameraBazaarContext();
            this.service = new CamerasService(db);
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
            if (db.Users.Any(u => u.Username == rubm.Username || u.Email == rubm.Email))
            {
                this.ModelState.AddModelError("UsernameEmail", "* There is already a user with such username/email");
                return View(rubm);
            }

            User user = new User
            {
                Email = rubm.Email,
                Phone = rubm.Phone,
                Password = rubm.Password,
                Username = rubm.Username
            };

            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Login", "Users");
            }

            return View("Register", rubm);
        }

        [Route("Login")]
        public ActionResult Login()
        {
            return View();
        }

        [Route("Login")]
        [HttpPost]
        public ActionResult Login([Bind(Exclude = "")] LoginUserBindingModel lubm)
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

            ModelState.AddModelError("WrongUsernamePassword", "* Incorrect username or password. Try again!");
            return View();
        }

        [Route("Logout")]
        public ActionResult Logout()
        {
            SignInManager.Logout(this.Session.SessionID);
            return RedirectToAction("Index", "Home");
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            UserDetailsViewModel userDetVm = service.GetUserDetails(id, this.Session);
            return View(userDetVm);
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            if (db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User.Id != id)
            {
                return RedirectToAction("Index", "Home");
            }
            User user = db.Users.Find(id);
            var userVm = new EditProfileViewModel
            {
                Email = user.Email,
                Id = user.Id,
                CurrentPassword = user.Password,
                Phone = user.Phone
            };

            return View(userVm);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "")] EditUserBindingModel eubm)
        {
            User currentUser = db.Sessions.FirstOrDefault(s => s.SessionId == this.Session.SessionID).User;
            var userVm = new EditProfileViewModel
            {
                Email = eubm.Email,
                Id = eubm.Id,
                CurrentPassword = eubm.CurrentPassword,
                Phone = eubm.Phone
            };
            if (currentUser.Id != eubm.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            if (currentUser.Password != eubm.CurrentPassword)
            {
                ModelState.AddModelError("InvalidOldPassword", "The password does not match your current password!");
                return View(userVm);
            }
            if (eubm.NewPassword == currentUser.Password)
            {
                ModelState.AddModelError("SamePasswords", "The new password is same as your old password!");
                return View(userVm);
            }

            currentUser.Email = eubm.Email;
            currentUser.Password = eubm.NewPassword;
            currentUser.Phone = eubm.Phone;

            if (ModelState.IsValid)
            {
                db.Entry(currentUser).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Users", new {id = eubm.Id});
            }

            return View(userVm);
        }
    }
}