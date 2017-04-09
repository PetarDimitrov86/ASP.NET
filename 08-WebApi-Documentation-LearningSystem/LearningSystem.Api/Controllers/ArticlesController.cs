namespace LearningSystem.Api.Controllers
{
    using System.Web.Http;
    using System.Collections.Generic;
    using System.Net;
    using Models.ViewModels.Article;
    using Microsoft.AspNet.Identity;
    using Models.ViewModels.Api.Articles;
    using Services.Contracts;

    /// <summary>
    /// Blog Articles Controller
    /// </summary>
    [RoutePrefix("api/articles")]
    public class ArticlesController : ApiController
    {
        private IBlogService service;

        public ArticlesController(IBlogService service)
        {
            this.service = service;
        }

        /// <summary>
        /// Displays all articles with their Id and Name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route]
        public IHttpActionResult ShowAllArticles()
        {
            IEnumerable<AllArticlesApiViewModel> articlesVms = this.service.ShowAllArticlesApi();
            return Ok(articlesVms);
        }

        /// <summary>
        /// Displays details about a particular Article
        /// </summary>
        /// <param name="id">Article Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult ArticleDetails(int id)
        {
            if (!this.service.ArticleExists(id))
            {
                return NotFound();
            }

            ArticleDetailsViewModel articlesVms = this.service.GetArticleDetails(id);
            return Ok(articlesVms);
        }

        /// <summary>
        /// Delete a selected article if its author is the currently logged in User
        /// </summary>
        /// <param name="id">Article Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "BlogAuthor")]
        [Route("{id:int}")]
        public IHttpActionResult DeleteArticle(int id)
        {
            var currentUserId = RequestContext.Principal.Identity.GetUserId();

            if (!this.service.ArticleExists(id))
            {
                return NotFound();
            }
            if (!this.service.ArticleAuthorIsCurrentUser(id, currentUserId))
            {
                return BadRequest("You can only delete your own articles!");
            }
            this.service.DeleteArticle(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Blog Authors can publish new articles
        /// </summary>
        /// <param name="aabm">Title, Content, ImageUrl</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "BlogAuthor")]
        [Route("publish")]
        public IHttpActionResult PublishArticle([FromBody] AddArticleViewModel aabm)
        {
            var currentUserId = RequestContext.Principal.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.service.AddArticle(currentUserId, aabm);
            return StatusCode(HttpStatusCode.Created);
        }
    }
}
