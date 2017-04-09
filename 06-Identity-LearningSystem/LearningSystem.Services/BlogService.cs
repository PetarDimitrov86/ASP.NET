namespace LearningSystem.Services
{
    using Models.ViewModels.Article;
    using System;
    using Models.EntityModels;
    using System.Collections.Generic;
    using AutoMapper;
    using System.Linq;

    public class BlogService : Service
    {
        public void AddArticle(string currentUserId, AddArticleViewModel aavm)
        {
            var appUser = this.Context.Users.Find(currentUserId);
            Article article = new Article
            {
                Content = aavm.Content,
                PublishDate = DateTime.Now,
                Title = aavm.Title,
                Author = appUser,
                ImageUrl = aavm.ImageUrl
            };
            this.Context.Articles.Add(article);
            this.Context.SaveChanges();
        }

        public IEnumerable<ArticleHomepageViewModel> GetAllArticles(string authorName)
        {
            var articles = this.Context.Articles.Where(a => a.Author.Name.Contains(authorName)).OrderByDescending(a => a.PublishDate);
            IEnumerable<ArticleHomepageViewModel> articlesVms = Mapper.Map<IEnumerable<Article>,IEnumerable<ArticleHomepageViewModel>>(articles);
            return articlesVms;
        }

        public IEnumerable<ArticleHomepageViewModel> GetFilteredArticles(string content)
        {
            var articles = this.Context.Articles.Where(a => a.Title.Contains(content) || a.Content.Contains(content)).OrderByDescending(a => a.PublishDate);
            return Mapper.Map<IEnumerable<Article>, IEnumerable<ArticleHomepageViewModel>>(articles);     
        }

        public EditArticleViewModel GetArticleToManage(int id)
        {
            Article article = this.Context.Articles.Find(id);
            EditArticleViewModel eavm = Mapper.Map<EditArticleViewModel>(article);
            return eavm;
        }

        public void EditSelectedArticle(EditArticleViewModel eavm)
        {
            Article article = this.Context.Articles.Find(eavm.Id);
            article.Content = eavm.Content;
            article.Title = eavm.Title;
            this.Context.SaveChanges();
        }

        public void DeleteArticle(int id)
        {
            Article article = this.Context.Articles.Find(id);
            this.Context.Articles.Remove(article);
            this.Context.SaveChanges();
        }
    }
}