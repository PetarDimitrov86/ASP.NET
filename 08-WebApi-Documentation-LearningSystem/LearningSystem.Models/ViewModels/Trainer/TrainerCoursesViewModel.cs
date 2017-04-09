namespace LearningSystem.Models.ViewModels.Trainer
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TrainerCoursesViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Number of Students")]
        public int NumberOfStudents { get; set; }
    }
}