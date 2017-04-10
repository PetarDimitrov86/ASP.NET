using System;

namespace BookShopSystem.Models.EntityModels
{
    public class Purchase
    {
        public int Id { get; set; }

        public DateTime? DateOfPurchase { get; set; }

        public bool IsRecalled { get; set; }

        public virtual Book Book { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}