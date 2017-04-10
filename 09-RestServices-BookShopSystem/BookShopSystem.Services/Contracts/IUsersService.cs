using BookShopSystem.Models.ViewModels.Users;

namespace BookShopSystem.Services.Contracts
{
    using System.Collections.Generic;

    public interface IUsersService
    {
        bool UserExists(string username);
        IEnumerable<UserPurchaseViewModel> GetAllUserPurhases(string username);
        bool RoleExists(string roleName);
        void AddRole(string username, string roleName);
        void RemoveRole(string username, string artubm);
    }
}