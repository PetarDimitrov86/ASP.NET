namespace LearningSystem.Models.ViewModels
{
    using System;
    using EntityModels;

    public class CertificateViewModel
    {
        public string CourseName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string StudentName { get; set; }

        public GradeType? Grade { get; set; }

        public string TrainerName { get; set; }
    }
}