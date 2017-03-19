namespace CameraBazaar.App.Models.BindingModels
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class EditCameraBindingModel
    {
        public int Id { get; set; }

        [DisplayName("Price: ")]
        [RegularExpression(@"^\d+\.\d{0,2}$")]
        public decimal Price { get; set; }

        [DisplayName("Quantity: ")]
        [Range(0, 100)]
        public int Quantity { get; set; }

        [DisplayName("Description: ")]
        [StringLength(6000)]
        public string Description { get; set; }
    }
}