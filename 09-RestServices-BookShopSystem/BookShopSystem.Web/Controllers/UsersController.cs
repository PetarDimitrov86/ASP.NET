namespace BookShopSystem.Web.Controllers
{
    using System.Net;
    using System.Web.Http;
    using Services.Contracts;
    using System.Collections.Generic;
    using Models.BindingModels.Users;
    using Models.ViewModels.Users;

    /// <summary>
    /// Users Controller
    /// </summary>
    [RoutePrefix("api/users")]
    public class UsersController : ApiController
    {
        private IUsersService _service;

        /// <summary>
        /// Creates an instance of the Users Service, by injecting with Ninject
        /// </summary>
        /// <param name="service">User Service Interface</param>
        public UsersController(IUsersService service)
        {
            this._service = service;
        }

        /// <summary>
        /// Returns a collection of user purchases
        /// </summary>
        /// <param name="username">User Username</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{username}/purchases")]
        public IHttpActionResult GetParticularUserPurchasesHistory(string username)
        {
            if (!this._service.UserExists(username))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }

            IEnumerable<UserPurchaseViewModel> purchasesVms = this._service.GetAllUserPurhases(username);
            return Ok(purchasesVms);
        }

        /// <summary>
        /// Add a role to a selected user
        /// </summary>
        /// <param name="username">User Username</param>
        /// <param name="artubm">Role Name</param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{username}/roles")]
        public IHttpActionResult AddRoleToUser(string username, [FromBody] AddRoleToUserBindingModel artubm)
        {
            if (!this._service.UserExists(username))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            if (!this._service.RoleExists(artubm.RoleName))
            {
                return BadRequest("There is no such role name.");
            }

            this._service.AddRole(username, artubm.RoleName);
            return StatusCode(HttpStatusCode.Created);
        }

        /// <summary>
        /// Removes a role from selected User
        /// </summary>
        /// <param name="username">User Username</param>
        /// <param name="artubm">Role Name</param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{username}/roles")]
        public IHttpActionResult RemoveRoleFromUser(string username, [FromBody] AddRoleToUserBindingModel artubm)
        {
            if (!this._service.UserExists(username))
            {
                return StatusCode(HttpStatusCode.NoContent);
            }
            if (!this._service.RoleExists(artubm.RoleName))
            {
                return BadRequest("There is no such role name.");
            }

            this._service.RemoveRole(username, artubm.RoleName);
            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}