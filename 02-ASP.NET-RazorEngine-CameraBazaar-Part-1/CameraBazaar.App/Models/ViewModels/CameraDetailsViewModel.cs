using System.ComponentModel;

namespace CameraBazaar.App.Models.ViewModels
{
    using System.Collections.Generic;

    public class CameraDetailsViewModel
    {
        public string Make { get; set; }

        public string Model { get; set; }

        public decimal Price { get; set; }

        public bool InStock { get; set; }

        public int SellerId { get; set; }

        public string SellerName { get; set; }

        public string ImageUrl { get; set; }

        [DisplayName("Is Full Frame: ")]
        public bool IsFullFrame { get; set; }

        [DisplayName("Min Shutter Speed: ")]
        public int MinShutterSpeed { get; set; }

        [DisplayName("Max Shutter Speed: ")]
        public int MaxShutterSpeed { get; set; }

        [DisplayName("Min ISO: ")]
        public int MinISO { get; set; }

        [DisplayName("Max ISO: ")]
        public int MaxISO { get; set; }

        [DisplayName("Video Resolution: ")]
        public string VideoResolution { get; set; }

        [DisplayName("Light Metering: ")]
        public ICollection<string> LightMetering { get; set; }

        [DisplayName("Description: ")]
        public string Description { get; set; }

    }
}