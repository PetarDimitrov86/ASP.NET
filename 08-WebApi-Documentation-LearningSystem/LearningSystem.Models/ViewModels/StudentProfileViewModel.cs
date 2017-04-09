namespace LearningSystem.Models.ViewModels
{
    using System.Collections.Generic;

    public class StudentProfileViewModel
    {
        public StudentProfileViewModel()
        {
            this.Courses = new HashSet<ProfileCourseViewModel>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProfileCourseViewModel> Courses { get; set; }
    }
}