using System;

namespace BookShopSystem.Models.ViewModels.Users
{
    public class UserPurchaseViewModel
    {
        public string BookTitle { get; set; }

        public decimal PurchasePrice { get; set; }

        public string Username { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public bool IsRecalled { get; set; }
    }
}