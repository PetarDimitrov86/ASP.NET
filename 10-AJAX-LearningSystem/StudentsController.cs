namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels;
    using Attributes;
    using System;
    using System.IO;
    using System.Web;
    using Rotativa;
    using Rotativa.Options;

    [RoutePrefix("Students")]
    [CustomAuthorize(Roles = "Student")]
    public class StudentsController : Controller
    {
        private IStudentsService service;

        public StudentsController(IStudentsService service)
        {
            this.service = service;
        }

        [Route("Profile")]
        public ActionResult Profile()
        {
            string currentUserId = User.Identity.GetUserId();
            StudentProfileViewModel studentVm = this.service.GetUserProfile(currentUserId);
            return View(studentVm);
        }

        [Route("Profile")]
        [HttpPost]
        public ActionResult Profile(int courseId, int studentId)
        {
            this.service.RemoveUserFromCourse(courseId, studentId);
            return RedirectToAction("Profile");
        }

        [Route("CoursesPartial")]
        public ActionResult CoursesPartial()
        {
            string currentUserId = User.Identity.GetUserId();
            StudentProfileViewModel studentVm = this.service.GetUserProfile(currentUserId);
            return this.PartialView("_StudentCoursesPartial", studentVm);
        }
    }
}