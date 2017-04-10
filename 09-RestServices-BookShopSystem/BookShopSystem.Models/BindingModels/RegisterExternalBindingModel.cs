using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Models.BindingModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}