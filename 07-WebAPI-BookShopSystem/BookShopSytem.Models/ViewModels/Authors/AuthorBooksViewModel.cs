using System;
using System.Collections.Generic;
using BookShopSytem.Models.EntityModels;

namespace BookShopSytem.Models.ViewModels.Authors
{
    public class AuthorBooksViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public EditionType EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        public virtual ICollection<string> CategoriesNames { get; set; }
    }
}