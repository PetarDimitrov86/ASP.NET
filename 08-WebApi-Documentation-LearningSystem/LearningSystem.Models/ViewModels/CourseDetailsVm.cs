namespace LearningSystem.Models.ViewModels
{
    using System;

    public class CourseDetailsVm
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string TrainerUserName { get; set; }

        public string TrailerName { get; set; }

        public int NumberOfStudentsEnrolled { get; set; }

        public bool IsStudentEnrolled { get; set; }
    }
}