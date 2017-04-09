namespace LearningSystem.Api.Controllers
{
    using System.Web.Http;
    using Services.Contracts;
    using System.Collections.Generic;
    using Models.ViewModels.Api.Students;
    using Models.BindingModels.Api;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Students Controller
    /// </summary>
    [RoutePrefix("api/students")]
    public class StudentsController : ApiController
    {
        private IStudentsService service;

        public StudentsController(IStudentsService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Gets all the courses, that the selected user is enrolled in
        /// </summary>
        /// <param name="studentId">Student Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{studentId}/courses")]
        public IHttpActionResult GetAllStudentCourses(int studentId)
        {
            if (!this.service.StudentExists(studentId))
            {
                return BadRequest("No such student in database!");
            }
            IEnumerable<EnrolledCoursesStudentViewModel> studentCoursesVms = this.service.GetStudentCourses(studentId);
            return Ok(studentCoursesVms);
        }

        /// <summary>
        /// Displays all the CURRENT student courses and their grades (if present)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("grades")]
        [Authorize(Roles = "Student")]
        public IHttpActionResult GetCurrentUserGrades()
        {
            var currentUserId = this.User.Identity.GetUserId();
            IEnumerable<CourseGradeViewModel> courseGradesVms = this.service.GetUserGrades(currentUserId);
            return Ok(courseGradesVms);
        }

        /// <summary>
        /// Edit User Birth Date and Name only
        /// </summary>
        /// <param name="espbm">BirthDate, Name</param>
        /// <returns></returns>
        [HttpPut]
        [Route("edit")]
        [Authorize]
        public IHttpActionResult EditUserInfo([FromBody] EditStudentProfileBindingModel espbm)
        {
            var currentUserId = this.User.Identity.GetUserId();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            service.EditNameAndDate(currentUserId, espbm.BirthDate, espbm.Name);
            return Ok();
        }
    }
}
