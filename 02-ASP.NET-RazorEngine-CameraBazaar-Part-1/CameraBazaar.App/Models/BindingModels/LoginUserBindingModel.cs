namespace CameraBazaar.App.Models.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginUserBindingModel
    {
        [DisplayName("Username: ")]
        [RegularExpression("^[a-zA-Z]{4,20}$", ErrorMessage = "Username can't be between less than 4 or more than 20 letters.")]
        public string Username { get; set; }

        [DisplayName("Password: ")]
        public string Password { get; set; }
    }
}