namespace LearningSystem.Web.Controllers
{
    using System.Web.Mvc;
    using Attributes;
    using Services.Contracts;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Trainer;
    using System.Collections.Generic;
    using System.IO;

    [RoutePrefix("Trainer")]
    [CustomAuthorize(Roles = "Trainer")]
    public class TrainerController : Controller
    {
        private ITrainerService service;

        public TrainerController(ITrainerService service)
        {
            this.service = service;
        }

        [Route("Courses")]
        public ActionResult Courses()
        {
            var currentUserId = User.Identity.GetUserId();
            IEnumerable<TrainerCoursesViewModel> tcvm = service.GetTrainerCourses(currentUserId);
            return this.View(tcvm);
        }

        [Route("Assess/{id=1}")]
        public ActionResult Assess(int id = 1)
        {
            var currentUserId = User.Identity.GetUserId();
            if (!service.IsCourseExisting(id) || !service.IsCurrentUserTrainerForCourse(currentUserId, id))
            {
                return RedirectToAction("Courses", "Trainer");
            }

            IEnumerable<AssessStudentViewModel> asvm = service.GetCourseStudents(id);

            foreach (var studentViewModel in asvm)
            {
                string expectedExamName = studentViewModel.CourseName.Replace(" ", "_");
                string expectedFileName = $"{expectedExamName}-{studentViewModel.Username}.rar";
                
                string path = Server.MapPath("~/Exams/");
                FileInfo fileInfo = new FileInfo(path + expectedFileName);
                if (fileInfo.Exists)
                {
                    studentViewModel.HasSubmittedExam = true;
                }
            }

            return this.View(asvm);
        }

        [Route("Assess/{id=1}")]
        [HttpPost]
        public ActionResult Assess(int courseId, int studentId, string gradeType)
        {
            service.EvaluateStudent(courseId, studentId, gradeType);
            return RedirectToAction("Assess", "Trainer", new { id = courseId});
        }
    }
}