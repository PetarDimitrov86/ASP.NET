namespace BookShopSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Net;
    using System.Web.Http;
    using Services.Contracts;
    using Models.BindingModels.Authors;
    using Models.ViewModels.Authors;
    using System.Linq;
    using System.Web.Http.OData;

    /// <summary>
    /// Author Controller
    /// </summary>
    [RoutePrefix("api/authors")]
    public class AuthorsController : ApiController
    {
        private IAuthorsService _service;

        /// <summary>
        /// Author service for helping with Author Controller Logic
        /// </summary>
        /// <param name="service"></param>
        public AuthorsController(IAuthorsService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Retrieves author details - Id, Name, List of Book titles
        /// </summary>
        /// <param name="id">Author Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult AuthorDetails(int id)
        {
            if (!this._service.ContainsAuthor(id))
            {
                return NotFound();
            }

            AuthorDetailsViewModel advm = this._service.GetAuthorDetails(id);
            return Ok(advm);
        }

        /// <summary>
        /// Allows admins to create new authors
        /// </summary>
        /// <param name="aabm">First and Last name for author</param>
        /// <returns></returns>
        [Route]
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult CreateAuthor([FromBody] AddAuthorBindingModel aabm)
        {
            if (ModelState.IsValid)
            {
                this._service.AddNewAuthor(aabm);
                return this.StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

        /// <summary>
        /// Retrieve the books of a particular author
        /// </summary>
        /// <param name="id">Author Id</param>
        /// <returns></returns>
        [Route("{id:int}/books")]
        public IHttpActionResult GetAuthorBooks(int id)
        {
            if (!this._service.ContainsBook(id))
            {
                return NotFound();
            }
            IEnumerable<AuthorBooksViewModel> abvm = this._service.GetAuthorBookDetails(id);
            return Ok(abvm);
        }

        [HttpGet]
        [EnableQuery]
        [Route("~/odata/authors/{id:int}/books")]
        // http://localhost:3658/odata/authors/3/books?$filter=substringof(tolower('ARms'), tolower(Title)) eq true
        public IQueryable<AuthorBooksViewModel> GetAuthorBooksOData(int id)
        {
            IQueryable<AuthorBooksViewModel> abvm = this._service.GetAuthorBookDetailsOData(id);
            return abvm;
        }
    }
}