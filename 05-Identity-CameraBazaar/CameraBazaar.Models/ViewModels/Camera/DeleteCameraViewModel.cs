namespace CameraBazaar.Models.ViewModels.Camera
{
    using EntityModels;

    public class DeleteCameraViewModel
    {
        public Make Make { get; set; }

        public string Model { get; set; }

        public int CameraId { get; set; }

        public int UserId { get; set; }
    }
}