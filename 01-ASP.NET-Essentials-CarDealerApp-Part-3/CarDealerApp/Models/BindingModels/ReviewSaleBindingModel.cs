namespace CarDealerApp.Models.BindingModels
{
    public class ReviewSaleBindingModel
    {
        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public double Discount { get; set; }
    }
}