namespace BookShopSystem.Services
{
    using AutoMapper;
    using Contracts;
    using BookShopSytem.Models.EntityModels;
    using BookShopSytem.Models.ViewModels.Books;
    using System.Collections.Generic;
    using System.Linq;
    using BookShopSytem.Models.BindingModels.Books;

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
    }
}