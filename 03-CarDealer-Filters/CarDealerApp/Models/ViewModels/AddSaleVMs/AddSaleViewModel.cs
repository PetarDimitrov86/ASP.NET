namespace CarDealerApp.Models.ViewModels.AddSaleVMs
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AddSaleViewModel
    {
        public AddSaleViewModel()
        {
            this.CustomerId = new HashSet<AddSaleCustomerViewModel>();
            this.CarId = new HashSet<AddSaleCarViewModel>();
            this.Discount = new List<int> { 0, 5, 10, 15, 20, 25, 30, 35, 40, 45, 50, 55, 60};
        }

        [DisplayName("Customer:")]
        public ICollection<AddSaleCustomerViewModel> CustomerId { get; set; }

        [DisplayName("Car:")]
        public ICollection<AddSaleCarViewModel> CarId { get; set; }

        [DisplayName("Discount:")]
        public ICollection<int> Discount { get; set; }
    }
}