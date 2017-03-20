namespace CarDealerApp.Models.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System;

    public class EditCustomerViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}