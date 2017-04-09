using System.Collections.Generic;
using BookShopSytem.Models.BindingModels.Books;
using BookShopSytem.Models.ViewModels.Books;

namespace BookShopSystem.Services.Contracts
{
    public interface IBooksService
    {
        bool ContainsBook(int id);
        BookDetailsViewModel GetBookDetails(int id);
        IEnumerable<SearchBookViewModel> GetTenSearchedBooks(string word);
        void UpdateBookInfo(int id, EditBookBindingModel ebbm);
        void DeleteBook(int id);
        void AddNewBook(AddBookBindingModel abbm);
    }
}