namespace CarDealerApp.Models.ViewModels
{
    using System.Collections.Generic;
    using AddSaleVMs;

    public class AddSaleViewModel
    {
        public AddSaleViewModel()
        {
            this.Customers = new HashSet<AddSaleCustomerViewModel>();
            this.Cars = new HashSet<AddSaleCarViewModel>();
        }

        public ICollection<AddSaleCustomerViewModel> Customers { get; set; }

        public ICollection<AddSaleCarViewModel> Cars { get; set; }
    }
}