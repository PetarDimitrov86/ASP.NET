namespace CarDealerApp.Models.BindingModels
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Collections.Generic;

    public class NewCarBindingModel
    {
        public NewCarBindingModel()
        {
            this.Parts = new HashSet<int>();
        }

        [MinLength(3, ErrorMessage = "Name cannot be shorter than 3 symbols")]
        public string Make { get; set; }

        [Range(0, Int64.MaxValue, ErrorMessage = "Cannot have a negative value for kilometers")]
        public long TravelledDistance { get; set; }

        [MinLength(3, ErrorMessage = "Name cannot be shorter than 3 symbols")]
        public string Model { get; set; }

        public ICollection<int> Parts { get; set; }
    }
}