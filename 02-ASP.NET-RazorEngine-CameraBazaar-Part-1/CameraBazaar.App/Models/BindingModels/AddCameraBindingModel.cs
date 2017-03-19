namespace CameraBazaar.App.Models.BindingModels
{
    using System.ComponentModel.DataAnnotations;
    using CameraBazaar.Models;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AddCameraBindingModel
    {
        public AddCameraBindingModel()
        {
            this.LightMeterings = new HashSet<int>();
        }

        [DisplayName("Make: ")]
        public Make Make { get; set; }

        [Required]
        [DisplayName("Model: ")]
        [RegularExpression("^[A-Z0-9-]+$")]
        public string Model { get; set; }

        [DisplayName("Price: ")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal Price { get; set; }

        [DisplayName("Quantity: ")]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [DisplayName("Min Shutter Speed: ")]
        [Range(1, 30)]
        public int MinShutterSpeed { get; set; }

        [DisplayName("Max Shutter Speed: ")]
        [Range(2000, 8000)]
        public int MaxShutterSpeed { get; set; }

        [DisplayName("Min ISO: ")]
        [RegularExpression(@"50|100")]
        public int MinISO { get; set; }

        [DisplayName("Max ISO: ")]
        [Range(200, 409600)]
        [RegularExpression(@"^\d{0,4}00$")]
        public int MaxISO { get; set; }

        [DisplayName("Full Frame: ")]
        public bool IsFullFrame { get; set; }

        [DisplayName("Video Resolution: ")]
        [StringLength(15)]
        public string VideoResolution { get; set; }

        [DisplayName("Light Metering: ")]
        public ICollection<int> LightMeterings { get; set; }

        [DisplayName("Description: ")]
        [StringLength(6000)]
        public string Description { get; set; }

        [DisplayName("Image URL: ")]
        [RegularExpression("^http://.+|https://.+")]
        public string ImageUrl { get; set; }
    }
}