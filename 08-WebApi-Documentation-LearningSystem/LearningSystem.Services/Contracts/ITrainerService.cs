namespace LearningSystem.Services.Contracts
{
    using System.Collections.Generic;
    using Models.ViewModels.Trainer;

    public interface ITrainerService
    {
        IEnumerable<TrainerCoursesViewModel> GetTrainerCourses(string currentUserId);
        IEnumerable<AssessStudentViewModel> GetCourseStudents(int id);
        void EvaluateStudent(int courseId, int studentId, string gradeType);
        bool IsCurrentUserTrainerForCourse(string currentUserId, int courseId);
        bool IsCourseExisting(int id);
    }
}