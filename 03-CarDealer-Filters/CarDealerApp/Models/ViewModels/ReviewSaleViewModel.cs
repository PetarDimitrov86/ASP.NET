namespace CarDealerApp.Models.ViewModels
{
    using System.ComponentModel;

    public class ReviewSaleViewModel
    {
        [DisplayName("Customer: ")]
        public string Customer { get; set; }

        [DisplayName("Car: ")]
        public string Car { get; set; }

        public int CustomerId { get; set; }

        public int CarId { get; set; }

        [DisplayName("Discount: ")]
        public string DiscountPercent { get; set; }
        
        public double? Discount { get; set; }

        [DisplayName("Price: ")]
        public double? Price { get; set; }

        [DisplayName("Final Price: ")]
        public double? FinalPrice { get; set; }
    }
}