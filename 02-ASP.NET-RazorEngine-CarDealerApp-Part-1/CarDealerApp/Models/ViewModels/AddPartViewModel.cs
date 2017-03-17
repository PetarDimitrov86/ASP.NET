namespace CarDealerApp.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AddPartViewModel
    {
        [DisplayName("Name: ")]
        public string Name { get; set; }

        [DisplayName("Price: ")]
        public decimal Price { get; set; }

        [DisplayName("Supplier: ")]
        public ICollection<AddPartSupplierViewModel> Supplier { get; set; }

        [DisplayName("Quantity: ")]
        public int Quantity { get; set; }
    }
}