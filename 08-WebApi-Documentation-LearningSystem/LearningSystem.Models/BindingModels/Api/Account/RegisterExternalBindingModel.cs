using System.ComponentModel.DataAnnotations;

namespace LearningSystem.Models.BindingModels.Api.Account
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}