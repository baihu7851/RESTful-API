using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using RESTfulAPI.Middleware.Interfaces;
using RESTfulAPI.ViewModel;

namespace RESTfulAPI.ApiController.Controllers
{
    [Route("api/Role")]
    [ApiController]
    public class RoleUserController : ControllerBase
    {
        private readonly IRoleUser _roleUser;

        public RoleUserController(IRoleUser roleUser)
        {
            _roleUser = roleUser;
        }

        // POST api/Role/Users/Add
        [HttpPost("Users/Add")]
        public ActionResult Post(int roleId, List<int> usersId)
        {
            var listRoleUser = new List<ViewRoleUser>();
            foreach (var i in usersId)
            {
                var viewRoleUser = new ViewRoleUser { RoleId = roleId, UserId = i };
                listRoleUser.Add(viewRoleUser);
            }
            var result = _roleUser.AddRoleUser(listRoleUser);
            return Ok(result);
        }

        // DELETE api/Role/Users/Delete
        [HttpDelete("Users/Delete")]
        public ActionResult Delete(int roleId, List<int> usersId)
        {
            var listRoleUser = new List<ViewRoleUser>();
            foreach (var i in usersId)
            {
                var viewRoleUser = new ViewRoleUser { RoleId = roleId, UserId = i };
                listRoleUser.Add(viewRoleUser);
            }
            var result = _roleUser.DeleteRoleUser(listRoleUser);
            return Ok(result);
        }
    }
}