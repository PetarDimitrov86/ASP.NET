namespace LearningSystem.Models.ViewModels.Article
{
    using System.ComponentModel.DataAnnotations;

    public class EditArticleViewModel
    {
        public int Id { get; set; }

        [Required, MinLength(5)]
        public string Title { get; set; }

        [Required, MinLength(40)]
        public string Content { get; set; }
    }
}