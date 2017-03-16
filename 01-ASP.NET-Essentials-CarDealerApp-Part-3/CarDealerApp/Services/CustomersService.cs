namespace CarDealerApp.Services
{
    using Models.BindingModels;
    using System;
    using CarDealer.Data;
    using CarDealer.Models;

    public class CustomersService
    {
        private CarDealerContext context;

        public CustomersService()
        {
            this.context = new CarDealerContext();
        }

        public Customer CreateNewCustomer(NewCustomerBindingModel ncbm)
        {
            Customer customer = new Customer
            {
                Name = ncbm.Name,
                BirthDate = ncbm.date,
                IsYoungDriver = IsDriverYoung(ncbm.date)
            };
            return customer;
        }
        private bool IsDriverYoung(DateTime date)
        {
            return DateTime.Now.Year - date.Year < 18;
        }

        public Customer EditExistingCustomer(EditCustomerBindingModel ecbm, CarDealerContext db)
        {
            Customer customer = db.Customers.Find(ecbm.Id);

            customer.Name = ecbm.Name;
            customer.BirthDate = ecbm.Date;

            if (DateTime.Now.Year - ecbm.Date.Year < 18)
            {
                customer.IsYoungDriver = true;
            }
            else
            {
                customer.IsYoungDriver = false;
            }
            return customer;
        }
    }
}