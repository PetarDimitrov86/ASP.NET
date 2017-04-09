namespace LearningSystem.Models.ViewModels.Api.Articles
{
    using System;

    public class ArticleDetailsViewModel
    {
        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime? PublishDate { get; set; }

        public string AuthorName { get; set; }
    }
}
