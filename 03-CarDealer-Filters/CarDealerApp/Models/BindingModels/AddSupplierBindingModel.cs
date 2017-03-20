using System.ComponentModel;

namespace CarDealerApp.Models.BindingModels
{
    public class AddSupplierBindingModel
    {
        [DisplayName("Is Importing?")]
        public bool Importer { get; set; }

        [DisplayName("Name: ")]
        public string Name { get; set; }
    }
}