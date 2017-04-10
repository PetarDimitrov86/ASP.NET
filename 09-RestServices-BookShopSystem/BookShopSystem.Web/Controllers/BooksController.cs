using BookShopSystem.Models.BindingModels.Books;
using BookShopSystem.Models.ViewModels.Books;

namespace BookShopSystem.Web.Controllers
{
    using System.Web.Http;
    using Services.Contracts;
    using System.Collections.Generic;
    using System.Net;
    using Microsoft.AspNet.Identity;

    /// <summary>
    /// Books Controller
    /// </summary>
    [RoutePrefix("api/books")]
    public class BooksController : ApiController
    {
        private IBooksService _service;

        /// <summary>
        /// Constructor for receiving an instance of the Book Service, to help with Book Controller Logic
        /// </summary>
        /// <param name="service"></param>
        public BooksController(IBooksService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Retrieves complete book details by given Id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
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

        /// <summary>
        /// Searches book titles for particular pattern
        /// </summary>
        /// <param name="search">Title name keyword</param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult Search(string search)
        {
            IEnumerable<SearchBookViewModel> bookDetailsVms = this._service.GetTenSearchedBooks(search);
            return Ok(bookDetailsVms);
        }

        /// <summary>
        /// Admins can update particular book details
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <param name="ebbm">Copies, Age Restriction, Description, Title, Price, etc</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Allows admin to delete a particular book by Id
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
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

        /// <summary>
        /// Allows creation of new books by admins
        /// </summary>
        /// <param name="abbm">Price, Copies, Title, Description, etc.</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IHttpActionResult CreateNewBook([FromBody] AddBookBindingModel abbm)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            this._service.AddNewBook(abbm);
            return Ok();
        }

        // Second Part - Rest Services - Identity

        /// <summary>
        /// Logged in users can purchase a book
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("buy/{id:int}")]
        public IHttpActionResult TryPurchaseBook(int id)
        {
            if (!this._service.ContainsBook(id))
            {
                return NotFound();
            }
            if (!this._service.BookHasRemainingCopies(id))
            {
                return BadRequest("There are no copies of that book currently in stock. Please try again later.");
            }

            var currentUserId = this.User.Identity.GetUserId();
            this._service.PurchaseBook(id, currentUserId);
            return this.StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// Books bought within the last 30 days can be recalled by logged in user
        /// </summary>
        /// <param name="id">Book Id</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("recall/{id:int}")]
        public IHttpActionResult TryRecallBook(int id)
        {
            var currentUserId = this.User.Identity.GetUserId();

            if (!this._service.ContainsBook(id))
            {
                return NotFound();
            }

            if (!this._service.HasBookBeenPurchasedFromStore(id, currentUserId))
            {
                return BadRequest("Sorry, you have not purchased this book from us, we cannot accept it.");
            }

            if (!this._service.CanBookBeRecalled(id, currentUserId))
            {
                return BadRequest("Sorry. 30 days have already passed since you have purchased the book.");
            }

            this._service.RecallBook(id, currentUserId);
            return Ok();
        }
    }
}