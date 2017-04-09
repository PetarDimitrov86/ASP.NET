namespace LearningSystem.Models.BindingModels
{
    using System;

    public class EditCourseBindingModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Trainer { get; set; }

        public int Id { get; set; }
    }
}