namespace CarDealerApp.Models.ViewModels
{
    public class ReviewSaleViewModel
    {
        public string CustomerName { get; set; }

        public string CarName { get; set; }

        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public string DiscountPercent { get; set; }

        public double? FinalDiscountPercent { get; set; }

        public double? Price { get; set; }

        public double? FinalPrice { get; set; }
    }
}