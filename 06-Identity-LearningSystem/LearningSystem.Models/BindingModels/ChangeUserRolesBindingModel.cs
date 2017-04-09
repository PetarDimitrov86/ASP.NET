using System.Collections.Generic;

namespace LearningSystem.Models.BindingModels
{
    public class ChangeUserRolesBindingModel
    {
        public string Id { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}