using System.ComponentModel;

namespace CameraBazaar.Models.ViewModels.Admin
{
    public class ManageSellersViewModel
    {
        public int Id { get; set; }

        [DisplayName("Allowed To Sell")]
        public bool CanSell { get; set; }

        public string Email { get; set; }

        [DisplayName("Number Of Offers")]
        public int NumberOfOffers { get; set; }
    }
}