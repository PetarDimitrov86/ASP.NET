using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookShopSystem.Models.ViewModels.Authors
{
    public class AuthorDetailsViewModel
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public ICollection<string> BookTitles { get; set; }
    }
}