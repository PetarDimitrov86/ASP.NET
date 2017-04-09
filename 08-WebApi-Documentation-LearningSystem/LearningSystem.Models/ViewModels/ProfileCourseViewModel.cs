namespace LearningSystem.Models.ViewModels
{
    using System;
    using EntityModels;

    public class ProfileCourseViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public GradeType? Grade { get; set; }

        public int? GradeId { get; set; }
    }
}