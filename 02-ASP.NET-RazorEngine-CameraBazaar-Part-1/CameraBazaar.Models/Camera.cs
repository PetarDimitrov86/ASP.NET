namespace CameraBazaar.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class Camera
    {
        public Camera()
        {
            this.LightMeterings = new HashSet<LightMetering>();
        }

        public int Id { get; set; }

        public Make Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public int Quantity { get; set; }

        public int MinShutterSpeed { get; set; }

        public int MaxShutterSpeed { get; set; }

        public int MinISO { get; set; }

        public int MaxISO { get; set; }

        public bool IsFullFrame { get; set; }

        public string VideoResolution { get; set; }     

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public virtual User Seller { get; set; }

        public virtual ICollection<LightMetering> LightMeterings { get; set; }
    }

    public enum Make
    {
        Canon,
        Nikon,
        Penta,
        Sony
    }
}