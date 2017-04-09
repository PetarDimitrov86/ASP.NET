namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Web.Mvc;
    using Services;
    using Models.ViewModels.Article;
    using Attributes;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    [RouteArea("Blog")]
    [RoutePrefix("Articles")]
    [CustomAuthorize]
    public class ArticlesController : Controller
    {
        private BlogService service;

        public ArticlesController()
        {
            this.service = new BlogService();
        }

        [Route("All/{authorName=}")]
        public ActionResult All(string authorName = "")
        {
            IEnumerable<ArticleHomepageViewModel> articleVms = service.GetAllArticles(authorName);
            return View(articleVms);
        }

        [Route("Search/{content=}")]
        [HttpPost]
        public ActionResult Search(string content = "")
        {
            IEnumerable<ArticleHomepageViewModel> articleVms = service.GetFilteredArticles(content);
            return this.View("All", articleVms);
        }

        [Route("Add")]
        [CustomAuthorize(Roles = "BlogAuthor")]
        public ActionResult Add()
        {
            return this.View();
        }

        [Route("Add")]
        [HttpPost]
        [CustomAuthorize(Roles = "BlogAuthor")]
        public ActionResult Add(AddArticleViewModel aavm)
        {
            var currentUserId = User.Identity.GetUserId();
            if (ModelState.IsValid)
            {
                this.service.AddArticle(currentUserId, aavm);
                return RedirectToAction("All", "Articles");
            }

            return this.View(aavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [Route("EditArticle/{id}")]
        public ActionResult EditArticle(int id)
        {
            EditArticleViewModel eavm = service.GetArticleToManage(id);
            return this.View(eavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [Route("EditArticle/{id}")]
        public ActionResult EditArticle(EditArticleViewModel eavm) 
        {
            if (ModelState.IsValid)
            {
                service.EditSelectedArticle(eavm);
                return RedirectToAction("All", "Articles", new { area = "Blog"});
            }

            return this.View(eavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [Route("DeleteArticle/{id}")]
        public ActionResult DeleteArticle(int id)
        {
            EditArticleViewModel eavm = service.GetArticleToManage(id);
            return this.View(eavm);
        }

        [CustomAuthorize(Roles = "Admin")]
        [HttpPost]
        [Route("DeleteArticle/{id}")]
        public ActionResult DeleteArticle(EditArticleViewModel eavm)
        {
            service.DeleteArticle(eavm.Id);
            return RedirectToAction("All", "Articles", new {area = "Blog"});
        }
    }
}