namespace CarDealerApp.Services
{
    using CarDealer.Data;
    using System.Linq;
    using Models.ViewModels;
    using Models.ViewModels.AddSaleVMs;
    using CarDealer.Models;
    using Models.BindingModels;

    public class SalesService
    {
        private CarDealerContext context;

        public SalesService()
        {
            this.context = new CarDealerContext();
        }

        public AddSaleViewModel GetAddSaleDetails()
        {
            var addSaleVm = new AddSaleViewModel();

            var allCustomers = context.Customers.ToList();
            var allCars = context.Cars.ToList();

            foreach (var customer in allCustomers)
            {
                addSaleVm.Customers.Add(new AddSaleCustomerViewModel
                {
                    Id = customer.Id,
                    Name = customer.Name
                });
            }

            foreach (var car in allCars)
            {
                addSaleVm.Cars.Add(new AddSaleCarViewModel
                {
                    Id = car.Id,
                    Name = $"{car.Make} - {car.Model}"
                });
            }

            return addSaleVm;
        }

        public ReviewSaleViewModel ReviewFinalOffer(AddSaleBindingModel asbm)
        {
            Car carToSale = context.Cars.Find(asbm.Car);
            Customer customer = context.Customers.Find(asbm.Customer);
            double? price = carToSale.Parts.Sum(p => p.Price);

            string discountPercent = $"{asbm.Discount}%";
            
            if (customer.IsYoungDriver)
            {
                discountPercent += " (+5%)";
                asbm.Discount += 5;
            }
            double? discountFinal = asbm.Discount/100.0;
            double? finalPrice = price - price*discountFinal;

            return new ReviewSaleViewModel
            {
                CarName = $"{carToSale.Make} {carToSale.Model}",
                CustomerName = customer.Name,
                CustomerId = customer.Id,
                CarId = carToSale.Id,
                DiscountPercent = discountPercent,
                FinalDiscountPercent = discountFinal,
                FinalPrice = finalPrice,
                Price = price
            };
        }

        public Sale FinalizeSale(ReviewSaleBindingModel rsbm, CarDealerContext db)
        {
            return new Sale
            {
                Car = db.Cars.Find(rsbm.CarId),
                Customer = db.Customers.Find(rsbm.CustomerId),
                Discount = rsbm.Discount
            };
        }
    }
}