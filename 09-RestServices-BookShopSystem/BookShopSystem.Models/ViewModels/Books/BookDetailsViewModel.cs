using System;
using System.Collections.Generic;
using BookShopSystem.Models.EntityModels;

namespace BookShopSystem.Models.ViewModels.Books
{
    public class BookDetailsViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public EditionType EditionType { get; set; }

        public decimal Price { get; set; }

        public int Copies { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public AgeRestriction AgeRestriction { get; set; }

        public string AuthorName { get; set; }

        public ICollection<string> CategoriesNames { get; set; }
    }
}