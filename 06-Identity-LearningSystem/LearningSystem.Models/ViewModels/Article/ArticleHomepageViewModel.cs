namespace LearningSystem.Models.ViewModels.Article
{
    using System;

    public class ArticleHomepageViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ImageUrl { get; set; }

        public DateTime? PublishDate { get; set; }

        public string AuthorName { get; set; }
    }
}