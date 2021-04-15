using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Middleware.Interfaces;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/User")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly IRoleUser _roleUser;

        public UserRoleController(IRoleUser roleUser)
        {
            _roleUser = roleUser;
        }

        // GET api/User/Roles/5
        //[HttpGet("Roles/{userId}", Name = nameof(GetRoleId))]
        //public ActionResult<List<int>> GetRoleId(int userId)
        //{
        //    return _roleUser.GetUserRole(userId);
        //}

        // POST api/User/Roles/Add
        [HttpPost("Roles/Add/{id}")]
        public ActionResult Post(int id, List<int> rolesId)
        {
            _roleUser.AddUserRole(id, rolesId);
            return Ok();
        }

        // DELETE api/User/Roles/Delete
        [HttpDelete("Roles/Delete/{id}")]
        public ActionResult Delete(int id, List<int> rolesId)
        {
            _roleUser.DeleteUserRole(id, rolesId);
            return NoContent();
        }
    }
}