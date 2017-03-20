namespace CarDealerApp.Services
{
    using CarDealer.Data;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Models;
    using Models.BindingModels;

    public class CarsService
    {
        private CarDealerContext context;
        private LogsService logsService;

        public CarsService()
        {
            context = new CarDealerContext();
            logsService = new LogsService();
        }

        public AddCarViewModel GetPartsForDropdown()
        {
            var allParts = context.Parts.ToList();
            var addCarVm = new AddCarViewModel();
            var partsVms = new List<PartForCarViewModel>();

            foreach (var part in allParts)
            {
                partsVms.Add(new PartForCarViewModel
                {
                    Id = part.Id,
                    Name = part.Name
                });
            }

            addCarVm.Parts = partsVms;
            return addCarVm;
        }

        public Car AddNewCar(NewCarBindingModel ncbm, CarDealerContext db)
        {
            Car car = new Car
            {
                Make = ncbm.Make,
                Model = ncbm.Model,
                TravelledDistance = ncbm.TravelledDistance,
            };

            foreach (var part in ncbm.Parts)
            {
                car.Parts.Add(db.Parts.Find(part));
            }

            return car;
        }

        public void GenerateLog(ModifiedTable modifiedTable, Operation operation, int userId)
        {
            this.logsService.GenerateLog(modifiedTable, operation, userId);
        }
    }
}