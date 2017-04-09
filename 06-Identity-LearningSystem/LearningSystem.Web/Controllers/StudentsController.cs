namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Services;
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
        private StudentsService service;

        public StudentsController()
        {
            this.service = new StudentsService();
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

        [Route("Upload")]
        [HttpPost]
        public ActionResult Upload(int id)
        {
            string currentUserId = User.Identity.GetUserId();
            StudentProfileViewModel studentVm = this.service.GetUserProfile(currentUserId);

            HttpPostedFileBase file = this.Request.Files["exam"];

            if (file.ContentLength > 2097152)
            {
                ModelState.AddModelError("", "File cannot be larger than 2Mb!");
                return this.View("Profile", studentVm);
            }

            string fileName = Path.GetFileName(file.FileName);
            if (!fileName.EndsWith(".rar"))
            {
                ModelState.AddModelError("", "Please upload only .rar files.");
                return this.View("Profile", studentVm);
            }
            string fullFileName = service.GetFullFileName(id, currentUserId, fileName);

            string path = Server.MapPath("~/Exams");
            string fullPath = Path.Combine(path, fullFileName);
            file.SaveAs(fullPath);
            return RedirectToAction("Profile", studentVm);
        }

        [Route("Edit")]
        public ActionResult Edit()
        {
            string currentUserId = User.Identity.GetUserId();
            EditUserViewModel editStudentVm = this.service.EditCurrentUser(currentUserId);
            return View(editStudentVm);
        }

        [Route("Edit")]
        [HttpPost]
        public ActionResult Edit(DateTime? birthDate, string name)
        {
            string currentUserId = User.Identity.GetUserId();
            this.service.EditNameAndDate(currentUserId, birthDate, name);
            return RedirectToAction("Profile", "Students");
        }

        [Route("Certificate/{gradeId?}")]
        public ActionResult Certificate(int gradeId = 1)
        {
            string currentUserId = User.Identity.GetUserId();
            StudentProfileViewModel studentVm = this.service.GetUserProfile(currentUserId);

            if (!this.service.DoesCertificateExistForCurrentUser(gradeId, currentUserId))
            {
                return this.View("Profile", studentVm);
            }

            CertificateViewModel certificateVm = this.service.GenerateCertificate(gradeId);

           //return View("CertificatePlain", certificateVm);

            return new ViewAsPdf("CertificatePlain", certificateVm)
            {
                FileName = $"{certificateVm.CourseName.Replace(" ", "_")}-{certificateVm.StudentName.Replace(" ", "_")}.pdf",
                PageSize = Size.A4
            };
        }
    }
}