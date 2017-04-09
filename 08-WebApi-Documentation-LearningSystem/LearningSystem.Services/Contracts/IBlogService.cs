namespace LearningSystem.Services.Contracts
{
    using System.Collections.Generic;
    using Models.ViewModels.Api.Articles;
    using Models.ViewModels.Article;

    public interface IBlogService
    {
        void AddArticle(string currentUserId, AddArticleViewModel aavm);
        IEnumerable<ArticleHomepageViewModel> GetAllArticles(string authorName);
        IEnumerable<ArticleHomepageViewModel> GetFilteredArticles(string content);
        EditArticleViewModel GetArticleToManage(int id);
        void EditSelectedArticle(EditArticleViewModel eavm);
        void DeleteArticle(int id);
        bool ArticleExists(int id);
        IEnumerable<AllArticlesApiViewModel> ShowAllArticlesApi();
        ArticleDetailsViewModel GetArticleDetails(int id);
        bool ArticleAuthorIsCurrentUser(int id, string currentUserId);
    }
}