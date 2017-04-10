using BookShopSystem.Models.BindingModels.Books;
using BookShopSystem.Models.ViewModels.Books;

namespace BookShopSystem.Services.Contracts
{
    using System.Collections.Generic;

    public interface IBooksService
    {
        bool ContainsBook(int id);
        BookDetailsViewModel GetBookDetails(int id);
        IEnumerable<SearchBookViewModel> GetTenSearchedBooks(string word);
        void UpdateBookInfo(int id, EditBookBindingModel ebbm);
        void DeleteBook(int id);
        void AddNewBook(AddBookBindingModel abbm);
        bool BookHasRemainingCopies(int id);
        void PurchaseBook(int id, string currentUserId);
        bool HasBookBeenPurchasedFromStore(int id, string currentUserId);
        bool CanBookBeRecalled(int id, string currentUserId);
        void RecallBook(int id, string currentUserId);
    }
}