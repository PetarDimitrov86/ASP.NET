using BookShopSytem.Models.BindingModels.Categories;

namespace BookShopSystem.Web.Controllers
{
    using System.Collections.Generic;
    using System.Web.Http;
    using Services;
    using Services.Contracts;
    using BookShopSytem.Models.ViewModels.Categories;

    [RoutePrefix("api/categories")]
    public class CategoriesController : ApiController
    {
        private ICategoriesService _service;

        public CategoriesController()
        {
            this._service = new CategoriesService();
        }

        [HttpGet]
        [Route]
        public IHttpActionResult AllCategories()
        {
            IEnumerable<AllCategoriesViewModel> allCategoriesVms = this._service.GetAllCategories();
            return this.Ok(allCategoriesVms);
        }

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

        [HttpPut]
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
        
        [HttpDelete]
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

        [HttpPost]
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