namespace LearningSystem.Services
{
    using Models.ViewModels;
    using AutoMapper;
    using Models.EntityModels;
    using System.Linq;
    using System;

    public class StudentsService : Service
    {
        public StudentProfileViewModel GetUserProfile(string currentUserId)
        {
            var currentStudent = this.Context.Students.First(s => s.UserId == currentUserId);
            StudentProfileViewModel studentVm = Mapper.Map<Student, StudentProfileViewModel>(currentStudent);
            foreach (var course in currentStudent.Courses)
            {
                ProfileCourseViewModel courseVm = Mapper.Map<Course, ProfileCourseViewModel>(course);
                var grade =
                    this.Context.Grades.FirstOrDefault(g => g.Course.Id == course.Id && g.Student.Id == currentStudent.Id);
                courseVm.Grade = grade?.CourseGrade;
                courseVm.GradeId = grade?.Id;
                studentVm.Courses.Add(courseVm);
            }
            return studentVm;
        }

        public void RemoveUserFromCourse(int courseId, int studentId)
        {
            Course course = this.Context.Courses.Find(courseId);
            Student student = this.Context.Students.Find(studentId);
            student.Courses.Remove(course);
            Context.SaveChanges();
        }

        public EditUserViewModel EditCurrentUser(string currentUserId)
        {
            var currentStudent = this.Context.Students.First(s => s.UserId == currentUserId);
            EditUserViewModel editVm = Mapper.Map<Student, EditUserViewModel>(currentStudent);
            return editVm;
        }

        public void EditNameAndDate(string currentUserId, DateTime? birthDate, string name)
        {
            var currentUser = Context.Users.Find(currentUserId);
            currentUser.Name = name;
            currentUser.BirthDate = birthDate;
            this.Context.SaveChanges();
        }

        public bool DoesCertificateExistForCurrentUser(int gradeId, string currentUserId)
        {
            Grade grade = this.Context.Grades.Find(gradeId);
            if (grade == null)
            {
                return false;
            }
            if (grade.Student.UserId != currentUserId)
            {
                return false;
            }
            if (grade.CourseGrade != GradeType.A && grade.CourseGrade != GradeType.B && grade.CourseGrade != GradeType.C)
            {
                return false;
            }
            return true;
        }

        public CertificateViewModel GenerateCertificate(int gradeId)
        {
            Grade grade = this.Context.Grades.Find(gradeId);
            ApplicationUser student = this.Context.Users.Find(grade.Student.UserId);
            Course course = this.Context.Courses.Find(grade.Course.Id);

            CertificateViewModel certificateVm = new CertificateViewModel
            {
                CourseName = course.Name,
                EndDate = course.EndDate,
                Grade = grade.CourseGrade,
                StartDate = course.StartDate,
                StudentName = student.Name,
                TrainerName = course.Trainer.Name              
            };
            return certificateVm;
        }

        public string GetFullFileName(int id, string currentUserId, string fileName)
        {
            Course course = this.Context.Courses.Find(id);
            string courseName = course.Name.Replace(" ", "_");

            var currentUser = Context.Users.Find(currentUserId);
            string studentUsername = currentUser.UserName.Replace(" ", "_");

            string fileExtension = fileName.Substring(fileName.LastIndexOf("."));

            return $"{courseName}-{studentUsername}{fileExtension}";
        }
    }
}