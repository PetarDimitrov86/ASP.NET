namespace CameraBazaar.App.Models.ViewModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditCameraViewModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [DisplayName("Price: ")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal Price { get; set; }

        public string Name { get; set; }

        [DisplayName("Quantity: ")]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [DisplayName("Description: ")]
        [StringLength(6000)]
        public string Description { get; set; }
    }
}