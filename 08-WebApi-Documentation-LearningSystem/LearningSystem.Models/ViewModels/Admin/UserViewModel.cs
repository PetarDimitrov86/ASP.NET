namespace LearningSystem.Models.ViewModels.Admin
{
    using System.Collections.Generic;

    public class UserViewModel
    {
        public UserViewModel()
        {
            this.Roles = new HashSet<string>();
        }

        public string Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}