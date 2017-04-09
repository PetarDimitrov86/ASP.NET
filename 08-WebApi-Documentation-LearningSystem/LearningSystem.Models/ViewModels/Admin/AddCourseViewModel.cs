namespace LearningSystem.Models.ViewModels.Admin
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class AddCourseViewModel
    {
        [Required]
        [MinLength(5)]
        public string Name { get; set; }

        [Required]
        [MinLength(50)]
        public string Description { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Starting Date")]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Ending Date")]
        public DateTime? EndDate { get; set; }

        [Required]
        public IEnumerable<TrainerDropdownViewModel> Trainer { get; set; }
    }
}