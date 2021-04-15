using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using RESTfulAPI.Middleware.Interfaces;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly IRoleUser _roleUser;
        private readonly ILogger<RoleController> _logger;

        public RoleUserController(IRoleUser roleUser, ILogger<RoleController> logger)
        {
            _roleUser = roleUser;
            _logger = logger;
        }

        // GET api/Role/Users/5
        //[HttpGet("Users/{roleId}")]
        //public ActionResult<List<int>> GetUserId(int roleId)
        //{
        //    return _roleUser.GetRoleUser(roleId);
        //}

        // POST api/Role/Users/Add
        [HttpPost("Users/Add")]
        public ActionResult Post(int roleId, List<int> usersId)
        {
            _roleUser.AddRoleUser(roleId, usersId);
            return Ok();
        }

        // DELETE api/Role/Users/Delete
        [HttpDelete("Users/Delete")]
        public ActionResult Delete(int roleId, List<int> usersId)
        {
            _roleUser.DeleteRoleUser(roleId, usersId);
            _logger.LogError($"角色 {roleId} 刪除 {usersId}");
            return NoContent();
        }
    }
}