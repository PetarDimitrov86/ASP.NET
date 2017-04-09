namespace LearningSystem.Models.ViewModels.Api.Courses
{
    using System;

    public class AllCoursesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string TrainerName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int NumberOfEnrolledStudents { get; set; }
    }
}
