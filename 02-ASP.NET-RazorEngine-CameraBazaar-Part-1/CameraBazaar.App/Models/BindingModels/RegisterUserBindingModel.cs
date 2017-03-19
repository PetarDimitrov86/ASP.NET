namespace CameraBazaar.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel;

    public class RegisterUserBindingModel
    {
        [DisplayName("Username: ")]
        [RegularExpression("^[a-zA-Z]{4,20}$", ErrorMessage = "Username should be between 4 and 20 letters.")]
        public string Username { get; set; }

        [DisplayName("Email: ")]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email, e.g. example123@test.com")]
        public string Email { get; set; }

        [DisplayName("Phone: ")]
        [RegularExpression(@"^\+[0-9]{10,12}$", ErrorMessage = "Phone should start with + and have between 10 and 12 digits.")]
        public string Phone { get; set; }

        [DisplayName("Password: ")]
        [RegularExpression("^[a-z0-9]{3,}$", ErrorMessage = "Password should be longer than 3 symbols and can contain only small letters and digits.")]
        public string Password { get; set; }

        [DisplayName("Confirm Password: ")]
        [Compare("Password", ErrorMessage = "Password does not match.")]
        public string ConfirmPassword { get; set; }
    }
}