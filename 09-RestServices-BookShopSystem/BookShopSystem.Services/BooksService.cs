namespace BookShopSystem.Services
{
    using AutoMapper;
    using Contracts;
    using System.Collections.Generic;
    using System.Linq;
    using System;
    using Models.BindingModels.Books;
    using Models.EntityModels;
    using Models.ViewModels.Books;

    public class BooksService : Service, IBooksService
    {
        public bool ContainsBook(int id)
        {
            return this.Context.Books.Find(id) != null;
        }

        public BookDetailsViewModel GetBookDetails(int id)
        {
            Book book = this.Context.Books.Find(id);
            BookDetailsViewModel bdvm = Mapper.Instance.Map<BookDetailsViewModel>(book);
            return bdvm;
        }

        public IEnumerable<SearchBookViewModel> GetTenSearchedBooks(string word)
        {
            var books = this.Context.Books.Where(b => b.Title.Contains(word)).Take(10).OrderBy(b => b.Title);
            IEnumerable<SearchBookViewModel> bookDetailsVms =
                Mapper.Instance.Map<IEnumerable<SearchBookViewModel>>(books);
            return bookDetailsVms;
        }

        public void UpdateBookInfo(int id, EditBookBindingModel ebbm)
        {
            Book bookToUpdate = this.Context.Books.Find(id);
            Mapper.Map(ebbm, bookToUpdate);
            bookToUpdate.Author = this.Context.Authors.Find(ebbm.AuthorId);
            this.Context.SaveChanges();
        }

        public void DeleteBook(int id)
        {
            Book unwantedBook = this.Context.Books.Find(id);
            this.Context.Books.Remove(unwantedBook);
            this.Context.SaveChanges();
        }

        public void AddNewBook(AddBookBindingModel abbm)
        {
            Book book = Mapper.Map<Book>(abbm);
            string[] categoryNames = abbm.CategoryNames.Split(' ');

            foreach (var categoryName in categoryNames)
            {
                Category category = this.Context.Categories.FirstOrDefault(c => c.Name == categoryName);
                if (category != null)
                {
                    book.Categories.Add(category);
                }
            }

            this.Context.Books.Add(book);
            this.Context.SaveChanges();
        }

        public bool BookHasRemainingCopies(int id)
        {
            return this.Context.Books.Find(id).Copies > 0;
        }

        public void PurchaseBook(int id, string currentUserId)
        {
            ApplicationUser currentUser = this.Context.Users.Find(currentUserId);
            Book book = this.Context.Books.Find(id);

            Purchase purchase = new Purchase
            {
                Book = book,
                DateOfPurchase = DateTime.Now,
                User = currentUser
            };

            this.Context.Purchases.Add(purchase);
            book.Copies--;
            this.Context.SaveChanges();
        }

        public bool HasBookBeenPurchasedFromStore(int id, string currentUserId)
        {
            var purchases = this.Context.Purchases.Where(p => p.Book.Id == id && p.User.Id == currentUserId && p.IsRecalled == false);
            return purchases.Any();
        }

        public bool CanBookBeRecalled(int id, string currentUserId)
        {
            Purchase purchase = this.Context.Purchases.First(p => p.Book.Id == id && p.User.Id == currentUserId && p.IsRecalled == false);
            return (DateTime.Now - purchase.DateOfPurchase).Value.Days <= 30;
        }

        public void RecallBook(int id, string currentUserId)
        {
            Purchase purchase = this.Context.Purchases.First(p => p.Book.Id == id && p.User.Id == currentUserId && p.IsRecalled == false);
            purchase.Book.Copies++;
            purchase.IsRecalled = true;
            this.Context.SaveChanges();
        }
    }
}