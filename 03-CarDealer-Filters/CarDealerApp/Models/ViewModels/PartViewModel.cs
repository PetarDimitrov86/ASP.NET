namespace CarDealerApp.Models.ViewModels
{
    public class PartViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? Price { get; set; }

        public string SupplierName { get; set; }

        public int Quantity { get; set; }
    }
}