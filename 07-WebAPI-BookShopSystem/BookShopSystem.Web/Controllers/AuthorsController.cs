using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using BookShopSystem.Services;
using BookShopSystem.Services.Contracts;
using BookShopSytem.Models.BindingModels.Authors;
using BookShopSytem.Models.ViewModels.Authors;

namespace BookShopSystem.Web.Controllers
{
    [RoutePrefix("api/authors")]
    public class AuthorsController : ApiController
    {
        private IAuthorsService _service;

        public AuthorsController()
        {
            this._service = new AuthorsService();
        }

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

        [Route]
        [HttpPost]
        public IHttpActionResult CreateAuthor([FromBody]AddAuthorBindingModel aabm)
        {
            if (ModelState.IsValid)
            {
                this._service.AddNewAuthor(aabm);
                return this.StatusCode(HttpStatusCode.Created);
            }

            return BadRequest(ModelState);
        }

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
    }
}