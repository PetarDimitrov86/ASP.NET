namespace CarDealerApp.Models.ViewModels
{
    using System.Collections.Generic;
    using System.ComponentModel;

    public class AddCarViewModel
    {
        public AddCarViewModel()
        {
            this.Parts = new HashSet<PartForCarViewModel>();
        }

        [DisplayName("Make: ")]
        public string Make { get; set; }

        [DisplayName("Model: ")]
        public string Model { get; set; }

        [DisplayName("Total Distance Travelled: ")]
        public long TravelledDistance { get; set; }

        public ICollection<PartForCarViewModel> Parts { get; set; }
    }
}