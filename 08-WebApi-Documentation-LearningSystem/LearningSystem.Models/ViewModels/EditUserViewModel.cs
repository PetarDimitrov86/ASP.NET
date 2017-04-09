namespace LearningSystem.Models.ViewModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class EditUserViewModel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5, ErrorMessage = "Name cannot be less than 5 characters!")]
        [Display(Name = "Full Name")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime? BirthDate { get; set; }
    }
}