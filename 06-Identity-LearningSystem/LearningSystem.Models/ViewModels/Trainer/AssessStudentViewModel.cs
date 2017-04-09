namespace LearningSystem.Models.ViewModels.Trainer
{
    using EntityModels;

    public class AssessStudentViewModel
    {
        public int Id { get; set; }

        public GradeType? Grade { get; set; }

        public string Name { get; set; }

        public string Username { get; set; }

        public int CourseId { get; set; }

        public string CourseName { get; set; }

        public bool HasSubmittedExam { get; set; }

        public static bool IsCourseOver { get; set; }
    }
}