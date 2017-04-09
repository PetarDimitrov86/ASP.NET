namespace LearningSystem.Models.BindingModels.Api
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class EditStudentProfileBindingModel
    {
        [Required]
        public string Name { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
