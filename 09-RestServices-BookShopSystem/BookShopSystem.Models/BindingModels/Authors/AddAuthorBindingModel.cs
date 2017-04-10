using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Models.BindingModels.Authors
{
    public class AddAuthorBindingModel
    {
        public string FirstName  { get; set; }

        [Required]
        public string LastName { get; set; }
    }
}
