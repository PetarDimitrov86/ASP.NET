namespace CameraBazaar.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Mvc;
    using Models.ViewModels.Admin;
    using Data;
    using Models.BindingModels;
    using System.Linq;
    using Attributes;

    [RoutePrefix("Admin")]
    [AdminAuthorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private CameraBazaarContext context;

        public AdminController()
        {
            this.context = new CameraBazaarContext();
        }

        [Route("Manage")]
        public ActionResult Manage()
        {
            var sellersVm = new HashSet<ManageSellersViewModel>();
            string adminRoleId = context.Roles.First(ar => ar.Name == "Admin").Id;
            var sellersEntities = context.Sellers.Where(s => s.User.Roles.All(r => r.RoleId != adminRoleId));
            foreach (var seller in sellersEntities)
            {
                sellersVm.Add(new ManageSellersViewModel
                {
                    CanSell = seller.CanPostNewOffers,
                    Email = seller.User.Email,
                    Id = seller.Id,
                    NumberOfOffers = seller.Cameras.Count
                });
            }
            return View(sellersVm);
        }

        [Route("Manage")]
        [HttpPost]
        public ActionResult Manage([Bind(Exclude = "")] SimpleIdBindingModel sibm)
        {
            var seller = context.Sellers.Find(sibm.Id);

            seller.CanPostNewOffers = !seller.CanPostNewOffers;
            context.SaveChanges();

            return RedirectToAction("Manage");
        }
    }
}