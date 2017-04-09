namespace LearningSystem.Api.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using Models.BindingModels.Api.Course;
    using Models.ViewModels.Api.Courses;
    using Models.BindingModels;
    using Microsoft.AspNet.Identity;
    using Services.Contracts;

    /// <summary>
    /// Student Courses Controller
    /// </summary>
    [RoutePrefix("api/courses")]
    public class CoursesController : ApiController
    {
        private ICoursesService service;
        private IAdminService adminService;

        public CoursesController(ICoursesService courseService, IAdminService adminService)
        {
            this.service = courseService;
            this.adminService = adminService;
        }

        /// <summary>
        /// This is a GET method that returns all the courses.
        /// </summary>
        /// <returns>List of courses with their Id, Name, Trainer Name, Start Date, End Date and Number of Enrolled Students </returns>
        [HttpGet]
        [Route("all")]
        public IHttpActionResult GetAllCourses()
        {
            IEnumerable<AllCoursesViewModel> allCoursesVms = this.service.GetAllCoursesApi();
            return Ok(allCoursesVms);
        }

        /// <summary>
        /// Searches for a matching keyword in course Title
        /// </summary>
        /// <param name="keyword">Title Search Keyword</param>
        /// <returns>Filtered courses with their Id, Name</returns>
        [HttpGet]
        [Route]
        public IHttpActionResult SearchForCourse(string keyword)
        {
            IEnumerable<SearchCourseViewModel> searchCoursesVms = this.service.SearchForCourseApi(keyword);
            return Ok(searchCoursesVms);
        }

        /// <summary>
        /// Admins can add new courses
        /// </summary>
        /// <param name="ancbm">Name, StartDate, EndDate, TrainerId</param>
        /// <returns></returns>
        [HttpPost]
        [Route]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult AddNewCourse([FromBody] AddNewCourseBindingModel ancbm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!this.service.IsTrainerIdValid(ancbm.TrainerId))
            {
                return BadRequest("User is not a trainer or you have entered an invalid User Id!");
            }
            this.service.CreateNewCourse(ancbm);
            return StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// Displays full course details, including Name, Description, Trainer Name, Start Date and End Date
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult ViewCourseDetails(int id)
        {
            if (!this.service.DoesCourseExist(id))
            {
                return NotFound();
            }
            CourseDetailsApiViewModel courseDetApiVm = this.service.GetCourseDetailsApi(id);
            return Ok(courseDetApiVm);
        }

        /// <summary>
        /// Allows admins to edit an existing course
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <param name="ecabm">Course Name, Course Description, Trainer Id</param>
        /// <returns></returns>
        [HttpPut]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult EditCourse(int id, [FromBody] EditCourseApiBindingModel ecabm)
        {
            if (!this.service.DoesCourseExist(id))
            {
                return NotFound();
            }
            if (!this.service.IsTrainerIdValid(ecabm.TrainerId))
            {
                return BadRequest("User is not a trainer or you have entered an invalid User Id!");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            this.service.EditCourseInfo(id, ecabm);
            return Ok();
        }

        /// <summary>
        /// Admins can delete courses by their Id
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult DeleteCourse(int id)
        {
            if (!this.service.DoesCourseExist(id))
            {
                return NotFound();
            }
            this.adminService.DeleteCourse(new DeleteCourseBindingModel { Id = id});
            return this.StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Get short summary of students enrolled in selected course. Displays their Name and Id
        /// </summary>
        /// <param name="id">Course Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}/students")]
        public IHttpActionResult GetStudentsForSelectedCourse(int id)
        {
            if (!this.service.DoesCourseExist(id))
            {
                return NotFound();
            }
            IEnumerable<CourseEnrolledStudentViewModel> courseStudentVms =
                this.service.GetEnrolledStudentsInCourseApi(id);
            return Ok(courseStudentVms);
        }

        /// <summary>
        /// Enrolls the currently logged in User/Student
        /// </summary>
        /// <param name="courseId">Course Id</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Student")]
        [Route("{courseId}/enroll")]
        public IHttpActionResult EnrollStudent(int courseId)
        {
            if (!this.service.DoesCourseExist(courseId))
            {
                return NotFound();
            }

            var currentUserId = this.User.Identity.GetUserId();

            if (this.service.IsStudentEnrolledAlready(courseId, currentUserId))
            {
                return BadRequest("Student is already enrolled in selected course!");
            }

            this.service.EnrollStudentInCourse(courseId, currentUserId);
            return Ok();
        }
    }
}