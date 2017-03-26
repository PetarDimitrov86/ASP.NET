namespace CameraBazaar.Web.Controllers
{
    using System.Web.Mvc;
    using Data;
    using Models.ViewModels.Profile;
    using Services;
    using System.Data.Entity;
    using System.Linq;
    using Models.BindingModels;
    using Models.EntityModels;
    using Microsoft.AspNet.Identity;
    using System.Web;
    using Microsoft.AspNet.Identity.Owin;

    [RoutePrefix("Sellers")]
    [Authorize(Roles = "Seller")]
    public class SellersController : Controller
    {
        private CameraBazaarContext db;
        private CamerasService service;
        private ApplicationUserManager _userManager;

        public SellersController()
        {
            this.db = new CameraBazaarContext();
            this.service = new CamerasService(db);
        }
        public ApplicationUserManager UserManager
        {
            get { return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>(); }
            private set { _userManager = value; }  
        }

        [Route("Details/{id}")]
        public ActionResult Details(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            var currentUser = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId).User;
            UserDetailsViewModel userDetVm = service.GetUserDetails(id, currentUser);
            return View(userDetVm);
        }

        [Route("Edit/{id}")]
        public ActionResult Edit(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);

            ApplicationUser user = currentSeller.User;
            var userVm = new EditProfileViewModel
            {
                Email = user.Email,
                Id = currentSeller.Id,
                //CurrentPassword = user.Password,
                Phone = user.PhoneNumber
            };

            return View(userVm);
        }

        [Route("Edit/{id}")]
        [HttpPost]
        public ActionResult Edit([Bind(Exclude = "")] EditSellerBindingModel esbm)
        {
            string currentUserId = User.Identity.GetUserId();
            Seller currentSeller = db.Sellers.FirstOrDefault(s => s.User.Id == currentUserId);

            var userVm = new EditProfileViewModel
            {
                Email = esbm.Email,
                Id = esbm.Id,
                CurrentPassword = esbm.CurrentPassword,
                Phone = esbm.Phone
            };
            if (currentSeller.Id != esbm.Id)
            {
                return RedirectToAction("Index", "Home");
            }
            //if (currentSeller.User.Password != eubm.CurrentPassword)
            //{
            //    ModelState.AddModelError("InvalidOldPassword", "The password does not match your current password!");
            //    return View(userVm);
            //}
            //if (eubm.NewPassword == currentUser.Password)
            //{
            //    ModelState.AddModelError("SamePasswords", "The new password is same as your old password!");
            //    return View(userVm);
            //}

            currentSeller.User.Email = esbm.Email;
            //currentUser.Password = eubm.NewPassword;
            currentSeller.User.PhoneNumber = esbm.Phone;

            if (ModelState.IsValid)
            {
                db.Entry(currentSeller).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Sellers", new { id = esbm.Id });
            }

            return View(userVm);
        }

        [Route("LastLogin/{id}")]
        [ChildActionOnly]
        public ActionResult LastLogin(int id)
        {
            var seller = db.Sellers.Find(id);
            string result = string.Empty;
            result = seller.User.LastLoginTime == null ? "First log in" : $"Last login: {seller.User.LastLoginTime}";

            return this.PartialView("_LastLogin", result);
        }
    }
}