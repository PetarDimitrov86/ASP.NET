namespace LearningSystem.Models.ViewModels.Admin
{
    using System.ComponentModel.DataAnnotations;

    using System;

    public class ManageCourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Display(Name = "Trainer Name")]
        public string TrainerName { get; set; }
    }
}