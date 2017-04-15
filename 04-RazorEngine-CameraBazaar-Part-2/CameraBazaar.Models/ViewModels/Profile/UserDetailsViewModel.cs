namespace CameraBazaar.Models.ViewModels.Profile
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using Camera;

    public class UserDetailsViewModel
    {
        public UserDetailsViewModel()
        {
            this.UserCameras = new HashSet<GeneralCameraViewModel>();
        }

        [DisplayName("Username: ")]
        public string Username { get; set; }

        [DisplayName("Email: ")]
        public string Email { get; set; }

        [DisplayName("Username: ")]
        public string Phone { get; set; }

        [DisplayName("Cameras: ")]
        public int CamerasInStock { get; set; }

        public int CamerasOutOfStock { get; set; }

        public int SellerId { get; set; }

        public int CurrentUserId { get; set; }

        [DisplayName("Cameras: ")]
        public IEnumerable<GeneralCameraViewModel> UserCameras { get; set; }
    }
}