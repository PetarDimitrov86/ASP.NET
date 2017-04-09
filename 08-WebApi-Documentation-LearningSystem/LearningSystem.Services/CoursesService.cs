namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using Models.ViewModels;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;
    using Models.BindingModels.Api.Course;
    using Models.ViewModels.Api.Courses;
    using Contracts;

    public class CoursesService : Service, ICoursesService
    {
        public IEnumerable<CourseHomepageViewModel> GetAllCourses(string filter)
        {
            var courseEntities = Context.Courses.Where(c => c.Name.Contains(filter)).ToList();
            IEnumerable<CourseHomepageViewModel> coursVms =
                Mapper.Map<IEnumerable<Course>, IEnumerable<CourseHomepageViewModel>>(courseEntities);

            return coursVms;

            //return Mapper.Map<IEnumerable<Course>, IEnumerable<CourseHomepageViewModel>>(Context.Courses);
        }

        public CourseDetailsVm GetCourseDetails(int id, string userId)
        {
            var currentStudent = this.Context.Students.FirstOrDefault(s => s.User.Id == userId);
            Course course = this.Context.Courses.Find(id);
            if (course == null)
            {
                return null;
            }
            CourseDetailsVm courseVm = Mapper.Map<Course, CourseDetailsVm>(course);
            if (currentStudent != null && currentStudent.Courses.Any(c => c.Id == id))
            {
                courseVm.IsStudentEnrolled = true;
            }
            return courseVm;
        }

        public void EnrollStudentInCourse(int id, string userId)
        {
            Student student = this.Context.Students.First(s => s.User.Id == userId);
            Course course = this.Context.Courses.Find(id);
            if (!IsStudentEnrolledAlready(id, userId))
            {
                student.Courses.Add(course);
            }
            this.Context.SaveChanges();
        }

        public bool IsStudentEnrolledAlready(int id, string userId)
        {
            Student student = this.Context.Students.First(s => s.User.Id == userId);
            return student.Courses.Any(c => c.Id == id);
        }

        // WEB API Services

        public IEnumerable<AllCoursesViewModel> GetAllCoursesApi()
        {
            var courses = this.Context.Courses.ToList();
            IEnumerable<AllCoursesViewModel> allCoursesVms =
                Mapper.Instance.Map<IEnumerable<AllCoursesViewModel>>(courses);
            return allCoursesVms;
        }

        public IEnumerable<SearchCourseViewModel> SearchForCourseApi(string keyword)
        {
            var filteredCourses = this.Context.Courses.Where(c => c.Name.Contains(keyword)).ToList();
            IEnumerable<SearchCourseViewModel> searchCoursesVms =
                Mapper.Instance.Map<IEnumerable<SearchCourseViewModel>>(filteredCourses);

            return searchCoursesVms;
        }

        public bool IsTrainerIdValid(int trainerId)
        {
            Student student = this.Context.Students.Find(trainerId);

            if (student == null)
            {
                return false;
            }

            string roleId = this.Context.Roles.First(r => r.Name == "Trainer").Id;

            if (!student.User.Roles.Any(r => r.RoleId == roleId))
            {
                return false;
            }

            return true;
        }

        public void CreateNewCourse(AddNewCourseBindingModel ancbm)
        {
            ApplicationUser trainer = this.Context.Students.Find(ancbm.TrainerId)?.User;
            Course course = Mapper.Map<Course>(ancbm);
            course.Trainer = trainer;
            this.Context.Courses.Add(course);
            this.Context.SaveChanges();
        }

        public bool DoesCourseExist(int id)
        {
            return this.Context.Courses.Find(id) != null;
        }

        public CourseDetailsApiViewModel GetCourseDetailsApi(int id)
        {
            Course course = this.Context.Courses.Find(id);
            CourseDetailsApiViewModel courseDetailsApiVm = Mapper.Map<CourseDetailsApiViewModel>(course);
            return courseDetailsApiVm;
        }

        public void EditCourseInfo(int id, EditCourseApiBindingModel ecabm)
        {
            Course course = this.Context.Courses.Find(id);
            ApplicationUser trainer = this.Context.Students.Find(ecabm.TrainerId)?.User;

            course.Name = ecabm.Name;
            course.Description = ecabm.Description;
            course.Trainer = trainer;

            this.Context.SaveChanges();
        }

        public void DeleteCourse(int id)
        {
            Course course = this.Context.Courses.Find(id);
            this.Context.Courses.Remove(course);
            this.Context.SaveChanges();
        }

        public IEnumerable<CourseEnrolledStudentViewModel> GetEnrolledStudentsInCourseApi(int id)
        {
            Course course = this.Context.Courses.Find(id);
            IEnumerable<CourseEnrolledStudentViewModel> courseStudentsVms =
                Mapper.Map<IEnumerable<CourseEnrolledStudentViewModel>>(course.Students);
            return courseStudentsVms;
        }
    }
}