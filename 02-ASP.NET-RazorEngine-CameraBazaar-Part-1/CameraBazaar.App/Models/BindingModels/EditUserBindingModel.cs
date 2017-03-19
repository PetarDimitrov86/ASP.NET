namespace CameraBazaar.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;

    public class EditUserBindingModel
    {
        public int Id { get; set; }

        [RegularExpression(@"^\+[0-9]{10,12}$", ErrorMessage = "Phone should start with + and have between 10 and 12 digits.")]
        public string Phone { get; set; }

        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email, e.g. example123@test.com")]
        public string Email { get; set; }

        public string CurrentPassword { get; set; }

        [RegularExpression("^[a-z0-9]{3,}$", ErrorMessage = "Password should be longer than 3 symbols and can contain only small letters and digits.")]
        public string NewPassword { get; set; }
    }
}