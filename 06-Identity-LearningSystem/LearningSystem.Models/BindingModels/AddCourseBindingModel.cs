using System;
using System.ComponentModel.DataAnnotations;
namespace LearningSystem.Models.BindingModels
{
    public class AddCourseBindingModel
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
        public string Trainer { get; set; }
    }
}
