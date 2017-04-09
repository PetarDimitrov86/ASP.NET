namespace LearningSystem.Models.EntityModels
{
    public class Grade
    {
        public int Id { get; set; }

        public GradeType? CourseGrade { get; set; }

        public virtual Student Student { get; set; }

        public virtual Course Course { get; set; }
    }

    public enum GradeType
    {
        A, B, C, D, E, F
    }
}