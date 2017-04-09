namespace LearningSystem.Models.ViewModels.Api.Courses
{
    using System;

    public class CourseDetailsApiViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TrainerName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
