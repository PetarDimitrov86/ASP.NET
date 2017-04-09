using System.Collections.Generic;
using BookShopSytem.Models.BindingModels.Books;

namespace BookShopSystem.Web.Controllers
{
    using System.Web.Http;
    using Services;
    using Services.Contracts;
    using BookShopSytem.Models.ViewModels.Books;

    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private IBooksService _service;

        public BooksController()
        {
            this._service = new BooksService();
        }

        [HttpGet]
        [Route("{id}")]
        public IHttpActionResult Details(int id)
        {
            if (!this._service.ContainsBook(id))
            {
                return NotFound();
            }
            BookDetailsViewModel bdvm = this._service.GetBookDetails(id);
            return Ok(bdvm);
        }

        [HttpGet]
        public IHttpActionResult Search(string search)
        {
            IEnumerable<SearchBookViewModel> bookDetailsVms = this._service.GetTenSearchedBooks(search);
            return Ok(bookDetailsVms);
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateBook(int id, [FromBody] EditBookBindingModel ebbm)
        {
            if (!this._service.ContainsBook(id))
            {
                return NotFound();
            }
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            this._service.UpdateBookInfo(id, ebbm);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteBook(int id)
        {
            if (!this._service.ContainsBook(id))
            {
                return NotFound();
            }
            this._service.DeleteBook(id);
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult CreateNewBook([FromBody] AddBookBindingModel abbm)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            this._service.AddNewBook(abbm);
            return Ok();
        }
    }
}