namespace BookShopSystem.Services
{
    using System.Collections.Generic;
    using Contracts;
    using System.Linq;
    using AutoMapper;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.EntityModels;
    using Models.ViewModels.Users;

    public class UsersService : Service, IUsersService
    {
        public bool UserExists(string username)
        {
            return this.Context.Users.Any(u => u.UserName == username);
        }

        public IEnumerable<UserPurchaseViewModel> GetAllUserPurhases(string username)
        {
            ApplicationUser user = this.Context.Users.First(u => u.UserName == username);
            var purchases = user.Purchases.OrderBy(p => p.DateOfPurchase);
            return Mapper.Instance.Map<IEnumerable<UserPurchaseViewModel>>(purchases);
        }

        public bool RoleExists(string roleName)
        {
            return this.Context.Roles.Any(r => r.Name == roleName);
        }

        public void AddRole(string username, string roleName)
        {
            ApplicationUser user = this.Context.Users.First(u => u.UserName == username);
            IdentityRole identityRole = this.Context.Roles.First(r => r.Name == roleName);

            user.Roles.Add(new IdentityUserRole { RoleId = identityRole.Id, UserId = user.Id });
            this.Context.SaveChanges();
        }

        public void RemoveRole(string username, string roleName)
        {
            ApplicationUser user = this.Context.Users.First(u => u.UserName == username);
            IdentityRole identityRole = this.Context.Roles.First(r => r.Name == roleName);

            var role = user.Roles.FirstOrDefault(r => r.RoleId == identityRole.Id);
            if (role != null)
            {
                user.Roles.Remove(role);
            }
            this.Context.SaveChanges();
        }
    }
}