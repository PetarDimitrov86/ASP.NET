namespace LearningSystem.Models.BindingModels.Api.Course
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class AddNewCourseBindingModel
    {
        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        public int TrainerId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}
