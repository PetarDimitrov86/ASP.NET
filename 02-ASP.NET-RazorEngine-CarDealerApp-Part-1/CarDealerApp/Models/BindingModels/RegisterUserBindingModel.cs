namespace CarDealerApp.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class RegisterUserBindingModel
    {
        [RegularExpression(@"^[a-zA-Z0-9]{5,}$")]
        public string Username { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9]+@[a-z]+\.[a-z]+$")]
        public string Email { get; set; }

        [MinLength(5)]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }
}