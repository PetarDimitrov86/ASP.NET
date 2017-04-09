using System.ComponentModel.DataAnnotations;

namespace BookShopSytem.Models.BindingModels
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}