namespace CameraBazaar.Models.ViewModels.Camera
{
    public class GeneralCameraViewModel
    {
        public int Id { get; set; }

        public string ImageUrl { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }
    }
}