namespace CarDealerApp.Services
{
    using CarDealer.Data;
    using Models.ViewModels;
    using System.Collections.Generic;
    using System.Linq;
    using CarDealer.Models;
    using Models.BindingModels;

    public class PartsService
    {
        private CarDealerContext context;

        public PartsService()
        {
            this.context = new CarDealerContext();
        }

        public IEnumerable<PartViewModel> GetAllParts()
        {
            var parts = context.Parts.ToList();
            var partsVm = new List<PartViewModel>();

            foreach (var part in parts)
            {
                var partVm = new PartViewModel
                {
                    Id = part.Id,
                    Name = part.Name,
                    Quantity = part.Quantity,
                    Price = part.Price,
                    SupplierName = part.Supplier.Name
                };
                partsVm.Add(partVm);
            }
            return partsVm;
        }

        public AddPartViewModel GetPartSuppliers()
        {
            var allSupplierEntities = context.Suppliers.ToList();
            var addPartVm = new AddPartViewModel();

            var suppliersVMs = new List<AddPartSupplierViewModel>();

            foreach (var supplier in allSupplierEntities)
            {
                var supplierVm = new AddPartSupplierViewModel
                {
                    Id = supplier.Id,
                    Name = supplier.Name
                };
                suppliersVMs.Add(supplierVm);
            }
            addPartVm.Supplier = suppliersVMs;
            return addPartVm;
        }

        public Part AddNewPart(NewPartBindingModel npbm)
        {
            return new Part
            {
                Name = npbm.Name,
                Price = npbm.Price,
                Quantity = npbm.Quantity,
            };
        }

        public DeletePartViewModel GetPartToDelete(int id)
        {
            Part part = context.Parts.Find(id);

            var delPartVm = new DeletePartViewModel
            {
                Name = part.Name,
                Id = part.Id
            };

            return delPartVm;
        }

        public void DeletePart(int id)
        {
            Part partToDelete = context.Parts.Find(id);
            context.Parts.Remove(partToDelete);
            context.SaveChanges();
        }

        public EditPartViewModel DisplayPartToEdit(int id)
        {
            Part part = context.Parts.Find(id);

            return new EditPartViewModel
            {
                Id = part.Id,
                Price = part.Price,
                Quantity = part.Quantity,
                Name = part.Name
            };
        }

        public Part GetEditedPart(EditPartBindingModel epbm, CarDealerContext ctx)
        {
            Part partToEdit = ctx.Parts.Find(epbm.Id);

            partToEdit.Price = epbm.Price;
            partToEdit.Quantity = epbm.Quantity;

            return partToEdit;
        }
    }
}