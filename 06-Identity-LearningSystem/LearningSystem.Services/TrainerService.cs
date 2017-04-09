using System.IO;

namespace LearningSystem.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Models.ViewModels.Trainer;
    using AutoMapper;
    using Models.EntityModels;
    using System;

    public class TrainerService : Service
    {
        public IEnumerable<TrainerCoursesViewModel> GetTrainerCourses(string currentUserId)
        {
            var assessableCourses = this.Context.Courses
                .Where(c => c.Trainer.Id == currentUserId);
            return Mapper.Map<IEnumerable<Course>, IEnumerable<TrainerCoursesViewModel>>(assessableCourses);
        }

        public IEnumerable<AssessStudentViewModel> GetCourseStudents(int id)
        {
            Course course = this.Context.Courses.Find(id);
            var assessStudentVms = new HashSet<AssessStudentViewModel>();
            var courseStudents = course.Students.ToList();
            foreach (var student in courseStudents)
            {
                assessStudentVms.Add(new AssessStudentViewModel
                {
                    Id = student.Id,
                    Name = student.User.Name,
                    Username = student.User.UserName,
                    Grade = this.Context.Grades.FirstOrDefault(g => g.Course.Id == course.Id && g.Student.Id == student.Id)?.CourseGrade,
                    CourseId = id,
                    CourseName = course.Name
                });
            }
            AssessStudentViewModel.IsCourseOver = course.EndDate.Value.DayOfYear <= DateTime.Now.DayOfYear;
            return assessStudentVms;
        }

        public void EvaluateStudent(int courseId, int studentId, string gradeType)
        {
            GradeType studentGrade = (GradeType)Enum.Parse(typeof(GradeType), gradeType);
            Student student = this.Context.Students.Find(studentId);
            Course course = this.Context.Courses.Find(courseId);
            Grade grade = new Grade
            {
               Course = course,
               Student = student,
               CourseGrade = studentGrade
            };
            this.Context.Grades.Add(grade);
            this.Context.SaveChanges();
        }

        public bool IsCurrentUserTrainerForCourse(string currentUserId, int courseId)
        {
            Course selectedCourse = this.Context.Courses.Find(courseId);
            return selectedCourse.Trainer.Id == currentUserId;
        }

        public bool IsCourseExisting(int id)
        {
            Course course = this.Context.Courses.FirstOrDefault(c => c.Id == id);
            return course != null;
        }
    }
}