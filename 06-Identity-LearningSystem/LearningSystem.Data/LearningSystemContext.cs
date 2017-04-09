namespace LearningSystem.Data
{
    using Models.EntityModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Data.Entity;

    public class LearningSystemContext : IdentityDbContext<ApplicationUser>
    {
        public LearningSystemContext()
            : base("name=LearningSystemContext", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Course> Courses { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Grade> Grades { get; set; }

        public static LearningSystemContext Create()
        {
            return new LearningSystemContext();
        }
    }
}