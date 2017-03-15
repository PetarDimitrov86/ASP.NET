namespace CarDealerApp.Services
{
    using CarDealer.Data;
    using System.Collections.Generic;
    using Models.ViewModels;
    using System.Linq;
    using CarDealer.Models;
    using Models.BindingModels;

    public class SuppliersService
    {
        private CarDealerContext context;

        public SuppliersService()
        {
            this.context = new CarDealerContext();
        }

        public IEnumerable<SupplierViewModel> FilterSuppliers(string type)
        {
            var suppliersVMs = new HashSet<SupplierViewModel>();
            var suppliers = context.Suppliers.AsEnumerable();
            if (type.ToLower() == "local")
            {
                suppliers = context.Suppliers.Where(s => !s.IsImporter);
            }
            else if (type.ToLower() == "importer")
            {
                suppliers = context.Suppliers.Where(s => s.IsImporter);
            }
            foreach (var supplier in suppliers)
            {
                suppliersVMs.Add(new SupplierViewModel
                {
                    Id = supplier.Id,
                    Importer = supplier.IsImporter ? "Yes" : "No",
                    Name = supplier.Name,
                    NumberOfParts = supplier.Parts.Count
                });
            }
            return suppliersVMs;
        }

        public EditSupplierViewModel GetSupplierToEditInfo(int id)
        {
            Supplier supplier = context.Suppliers.Find(id);

            return new EditSupplierViewModel
            {
                Id = supplier.Id,
                IsImporter = supplier.IsImporter,
                Name = supplier.Name
            };
        }

        public void EditSupplier(EditSupplierBindingModel esbm)
        {
            Supplier supplier = context.Suppliers.Find(esbm.Id);
            supplier.Name = esbm.Name;
            supplier.IsImporter = esbm.Importer;
            context.SaveChanges();
        }

        public DeleteSupplierViewModel ConfirmDeleteModel(int id)
        {
            Supplier supplier = context.Suppliers.Find(id);

            return new DeleteSupplierViewModel
            {
                Id = supplier.Id,
                Name = supplier.Name
            };
        }
    }
}