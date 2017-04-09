namespace LearningSystem.Services.Contracts
{
    using System;
    using System.Collections.Generic;
    using Models.ViewModels;
    using Models.ViewModels.Api.Students;

    public interface IStudentsService
    {
        StudentProfileViewModel GetUserProfile(string currentUserId);
        void RemoveUserFromCourse(int courseId, int studentId);
        EditUserViewModel EditCurrentUser(string currentUserId);
        void EditNameAndDate(string currentUserId, DateTime? birthDate, string name);
        bool DoesCertificateExistForCurrentUser(int gradeId, string currentUserId);
        CertificateViewModel GenerateCertificate(int gradeId);
        string GetFullFileName(int id, string currentUserId, string fileName);
        bool StudentExists(int studentId);
        IEnumerable<EnrolledCoursesStudentViewModel> GetStudentCourses(int studentId);
        IEnumerable<CourseGradeViewModel> GetUserGrades(string currentUserId);
    }
}