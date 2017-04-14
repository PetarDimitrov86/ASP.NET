namespace LearningSystem.Web.Areas.Blog.Controllers
{
    using System.Web.Mvc;
    using Services.Contracts;
    using Models.ViewModels.Article;
    using Attributes;
    using Microsoft.AspNet.Identity;
    using System.Collections.Generic;

    [RouteArea("Blog")]
    [RoutePrefix("Articles")]
    [CustomAuthorize]
    public class ArticlesController : Controller
    {
        private IBlogService service;

        public ArticlesController(IBlogService service)
        {
            this.service = service;
        }

        [Route("All/{authorName=}")]
        public ActionResult All(string authorName = "")
        {
            IEnumerable<ArticleHomepageViewModel> articleVms = service.GetAllArticles(authorName);
            return View(articleVms);
        }

        [Route("PartialArticles")]
        public ActionResult PartialArticles(string content = "")
        {
            IEnumerable<ArticleHomepageViewModel> articleVms = service.GetFilteredArticles(content);
            return this.PartialView("_ArticlePartial", articleVms);
        }

        [Route("Search/{content=}")]
        [HttpPost]
        public ActionResult Search(string content = "")
        {
            IEnumerable<ArticleHomepageViewModel> articleVms = service.GetFilteredArticles(content);
            return this.View("All", articleVms);
        }
    }
}