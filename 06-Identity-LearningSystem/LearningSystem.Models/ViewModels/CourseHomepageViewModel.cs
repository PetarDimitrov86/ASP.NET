namespace LearningSystem.Models.ViewModels
{
    using System;

    public class CourseHomepageViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public string TrainerName { get; set; }
    }
}