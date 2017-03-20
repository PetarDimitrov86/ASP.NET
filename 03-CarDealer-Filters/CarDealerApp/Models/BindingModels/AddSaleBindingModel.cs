namespace CarDealerApp.Models.BindingModels
{
    public class AddSaleBindingModel
    {
        public int CustomerId { get; set; }

        public int CarId { get; set; }

        public int Discount { get; set; }
    }
}