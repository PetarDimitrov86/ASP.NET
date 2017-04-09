namespace LearningSystem.Web.Areas.Admin.Controllers
{
    using System.Web.Mvc;
    using Services;
    using System.Collections.Generic;
    using Models.ViewModels.Admin;
    using Attributes;
    using Models.BindingModels;
    using System.Web;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    [RouteArea("Admin")]
    [RoutePrefix("Manage")]
    [CustomAuthorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private AdminService service;

        public ManageController()
        {
            this.service = new AdminService();
        }

        [Route("Courses")]
        public ActionResult Courses()
        {
            IEnumerable<ManageCourseViewModel> manageCourseVms = service.GetManageableCourses();
            return View(manageCourseVms);
        }

        [Route("AddCourse")]
        public ActionResult AddCourse()
        {
            AddCourseViewModel addCourseVm = service.GetAddCourseDetails();
            return View(addCourseVm);
        }

        [Route("AddCourse")]
        [HttpPost]
        public ActionResult AddCourse(AddCourseBindingModel acbm)
        {
            if (acbm.StartDate > acbm.EndDate)
            {
                ModelState.AddModelError("", "End Date cannot be before Start Date");
                return View(service.GetAddCourseDetails());
            }

            if (ModelState.IsValid)
            {
                this.service.AddNewCourse(acbm);
                return RedirectToAction("Courses", "Manage", new { area = "Admin"});
            }
            return View(service.GetAddCourseDetails());
        }

        [Route("EditCourse/{id}")]
        public ActionResult EditCourse(int id)
        {
            EditCourseViewModel editCourseVm = this.service.GetEditInfo(id);
            return View(editCourseVm);
        }

        [Route("EditCourse/{id}")]
        [HttpPost]
        public ActionResult EditCourse(EditCourseBindingModel ecbm)
        {
            if (ecbm.StartDate > ecbm.EndDate)
            {
                ModelState.AddModelError("", "End Date cannot be before Start Date");
                return View(service.GetEditInfo(ecbm.Id));
            }

            if (ModelState.IsValid)
            {
                this.service.EditCourse(ecbm);
                return RedirectToAction("Courses", "Manage", new { area = "Admin" });
            }
            return View(service.GetEditInfo(ecbm.Id));
        }

        [Route("DeleteCourse/{id}")]
        public ActionResult DeleteCourse(int id)
        {
            DeleteCourseViewModel delCourseVm = this.service.GetUnwantedCourse(id);
            return View(delCourseVm);
        }

        [Route("DeleteCourse/{id}")]
        [HttpPost]
        public ActionResult DeleteCourse(DeleteCourseBindingModel dcbm)
        {
            this.service.DeleteCourse(dcbm);
            return RedirectToAction("Courses", "Manage", new { area = "Admin" });
        }

        [Route("Users")]
        public ActionResult Users()
        {
            UserRolesViewModel urvm = service.GetAllUsersAndRoles();
            return this.View(urvm);
        }

        [Route("Users")]
        [HttpPost]
        public ActionResult Users([Bind(Include = "Id,Roles")] ChangeUserRolesBindingModel curbm)
        {
            var userManager = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            service.ChangeUserRoles(curbm.Id);

            foreach (var role in curbm.Roles)
            {
                userManager.AddToRoles(curbm.Id, role);
            }
            return RedirectToAction("Users", "Manage", new {area = "Admin"});
        }
    }
}