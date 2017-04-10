using BookShopSystem.Models.BindingModels.Categories;
using BookShopSystem.Models.ViewModels.Categories;

namespace BookShopSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Services.Contracts;

    /// <summary>
    /// Categories Controller
    /// </summary>
    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private ICategoriesService _service;

        /// <summary>
        /// Gets an instance of the service through Ninject
        /// </summary>
        /// <param name="service">Categories Service Interface</param>
        public CategoriesController(ICategoriesService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Retrieves a list of all categories - Names and Ids
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route]
        public IHttpActionResult AllCategories()
        {
            IEnumerable<AllCategoriesViewModel> allCategoriesVms = this._service.GetAllCategories();
            return this.Ok(allCategoriesVms);
        }

        /// <summary>
        /// Returns a particular category Id and Name
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IHttpActionResult GetCategoryDetails(int id)
        {
            if (!_service.ContainsCategory(id))
            {
                return NotFound();
            }
            AllCategoriesViewModel categoryVm = this._service.GetCategoryInfo(id);
            return Ok(categoryVm);
        }

        /// <summary>
        /// Allows admins to change a particular category Name
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="ecbm">Category Name</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public IHttpActionResult UpdateCategoryName(int id, [FromBody] EditCategoryBindingModel ecbm)
        {
            if (!_service.ContainsCategory(id))
            {
                return NotFound();
            }
            if (this._service.CategoryNameAlreadyExists(ecbm))
            {
                this.ModelState.AddModelError("", "Category with such a name already exists!");
                return BadRequest(this.ModelState);
            }
            this._service.UpdateCategoryName(id, ecbm);
            return Ok();
        }
        
        /// <summary>
        /// Delete a category by Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id:int}")]
        public IHttpActionResult DeleteCategory(int id)
        {
            if (!_service.ContainsCategory(id))
            {
                return NotFound();
            }
            this._service.DeleteCategory(id);
            return Ok();
        }

        /// <summary>
        /// Add a new category by given Name
        /// </summary>
        /// <param name="acbm">Category Name</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route]
        public IHttpActionResult AddCategory([FromBody] EditCategoryBindingModel acbm) 
        {
            if (this._service.CategoryNameAlreadyExists(acbm))
            {
                this.ModelState.AddModelError("", "Category with such a name already exists!");
                return BadRequest(this.ModelState);
            }
            this._service.AddNewCategory(acbm);
            return Ok();
        }
    }
}