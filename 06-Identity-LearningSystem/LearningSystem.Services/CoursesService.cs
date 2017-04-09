namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using Models.ViewModels;
    using System.Linq;
    using AutoMapper;
    using Models.EntityModels;

    public class CoursesService : Service
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
            student.Courses.Add(course);
            this.Context.SaveChanges();
        }
    }
}