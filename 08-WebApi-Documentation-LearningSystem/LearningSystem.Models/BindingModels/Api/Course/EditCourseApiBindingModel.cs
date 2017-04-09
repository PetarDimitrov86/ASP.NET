namespace LearningSystem.Models.BindingModels.Api.Course
{
    using System.ComponentModel.DataAnnotations;

    public class EditCourseApiBindingModel
    {
        [Required]
        [MinLength(10)]
        public string Name { get; set; }

        [Required]
        [MinLength(50), MaxLength(5000)]
        public string Description { get; set; }

        [Required]
        public int TrainerId { get; set; }
    }
}
