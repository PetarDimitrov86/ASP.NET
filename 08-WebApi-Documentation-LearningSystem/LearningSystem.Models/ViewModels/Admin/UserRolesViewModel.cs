namespace LearningSystem.Models.ViewModels.Admin
{
    using System.Collections.Generic;

    public class UserRolesViewModel
    {
        public UserRolesViewModel()
        {
            this.Users = new HashSet<UserViewModel>();
            this.Roles = new HashSet<RoleViewModel>();
        }

        public IEnumerable<RoleViewModel> Roles { get; set; }

        public ICollection<UserViewModel> Users { get; set; }
    }
}