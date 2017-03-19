namespace CameraBazaar.App.Models.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditProfileViewModel
    {
        [DisplayName("Email: ")]
        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-‌​]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$", ErrorMessage = "Please enter a valid email, e.g. example123@test.com")]
        public string Email { get; set; }

        public int Id { get; set; }

        [Required]
        public string CurrentPassword { get; set; }

        [DisplayName("New Password: ")]
        [Required]
        [RegularExpression("^[a-z0-9]{3,}$", ErrorMessage = "Password should be longer than 3 symbols and can contain only small letters and digits.")]
        public string NewPassword { get; set; }

        [DisplayName("Phone: ")]
        [Required]
        [RegularExpression(@"^\+[0-9]{10,12}$", ErrorMessage = "Phone should start with + and have between 10 and 12 digits.")]
        public string Phone { get; set; }
    }
}